﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/*
 * 사용법 : effector.Alpha(0.5f, 0f).And().Scale(0.5f, 0f).Then().Disable().Play();
 *          효과.커넥션.효과.커넥션....효과.커넥션.플레이 <이런식으로 써야함
 * 작동 : 이펙트들을 And()로 묶어서 큐 해놓으면 Then()전까지 전부 실행
 *        실행한 이펙트 중 제일 긴 이펙트가 끝날때 다음 Then()까지 읽어옴, 반복
 * EffectCurve] 인자 time : normalized 지난 시간 / 리턴값 : normalized 된 효과 value
 * 
 * play와 동시에 effectQueue를 싹다 비움. cancelPlayingEffect 가 true면 play시 기존 플레이중이던 효과를 취소함
 */
public class Effector : MonoBehaviour
{
    
    public bool cancelPlayingEffect = false;
    public delegate float EffectCurve(float time);

    public static EffectCurve IncreCurve = increCurve;
    public enum ChainType { AND, THEN, DONE }
    class Effect 
    { 
        public IEnumerator coroutine;
        public ChainType nextType;
        public float duration;
        public Effect(IEnumerator coroutine, float duration)
        {
            this.coroutine = coroutine;
            this.nextType = ChainType.DONE;
            this.duration = duration;
        }
    }
    List<Effect> effectList = new List<Effect>();
    bool isPlaying = false;
    SpriteRenderer spriteRenderer;
    IEnumerator mainCoroutine;

    Vector3 original_Scale;
    Vector3 original_Pos;
    float original_Alpha;
    float original_Roate;
    Color original_Color;
    Sprite[] sprites;

#region Scale
    public Effector Scale(float duration, Vector2 target)
    { 
        return Scale(duration, target, increCurve); 
    }
    public Effector Scale(float duration, float target)
    { 
        return Scale(duration, new Vector2(target,target), increCurve);
    }
    public Effector Scale(float duration, float target, EffectCurve Curve)
    { 
        return Scale(duration, new Vector2(target,target), Curve);
    }
    public Effector Scale(float duration, Vector2 target, EffectCurve Curve)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(ScaleCoroutine(duration,target,Curve), duration));
        return this;
    }
    IEnumerator ScaleCoroutine(float duration, Vector2 target, EffectCurve Curve)
    {
        float eTime = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(target.x,target.y,originalScale.z);
        while(eTime <= duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, Curve(eTime/duration));
            eTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
    #endregion

    #region Move
    public Effector Move(float duration, Vector2 offset)
    {
        return Move(duration, offset, increCurve);
    }
    public Effector Move(float duration, Vector2 offset, EffectCurve Curve)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(MoveCoroutine(duration, offset, Curve), duration));
        return this;
    }
    IEnumerator MoveCoroutine(float duration, Vector2 offset, EffectCurve Curve)
    {
        float eTime = 0f;
        Vector3 originalPos = transform.localPosition;
        Vector2 moveDir;
        while (eTime <= duration)
        {
            eTime += Time.deltaTime;
            moveDir = offset * (Curve(eTime / duration) - Curve((eTime - Time.deltaTime) / duration));
            transform.localPosition += new Vector3(moveDir.x, moveDir.y, 0);
            yield return null;
        }
    }
    #endregion

    #region MoveTo
    public Effector MoveTo(float duration, Vector2 targetPos)
    {
        return MoveTo(duration, targetPos, increCurve);
    }
    public Effector MoveTo(float duration, Vector2 targetPos, EffectCurve Curve)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(MoveToCoroutine(duration, targetPos, Curve), duration));
        return this;
    }
    IEnumerator MoveToCoroutine(float duration, Vector2 targetPos, EffectCurve Curve)
    {
        float eTime = 0f;
        Vector3 originalPos = transform.localPosition;
        while (eTime <= duration)
        {
            transform.localPosition = Vector3.Lerp(originalPos, targetPos, Curve(eTime / duration));
            eTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPos;
    }
    #endregion

    #region Alpha
    public Effector Alpha(float duration, float target)
    { 
        return Alpha(duration, target, increCurve); 
    }
    public Effector Alpha(float duration, float target, EffectCurve Curve)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(AlphaCoroutine(duration,target,Curve), duration));
        return this;
    }
    IEnumerator AlphaCoroutine(float duration, float target, EffectCurve Curve)
    {
        float eTime = 0f;
        Color c = spriteRenderer.color;
        float originalAlpha = c.a;
        while(eTime <= duration)
        {
            c.a = Mathf.Lerp(originalAlpha, target, Curve(eTime/duration));
            spriteRenderer.color = c;
            eTime += Time.deltaTime;
            yield return null;
        }
        c.a = target;
        spriteRenderer.color = c;
    }
#endregion

#region Rotate
    public Effector Rotate(float duration, float target)
    { 
        return RotateTo(duration, target + transform.eulerAngles.z, increCurve); 
    }
    public Effector Rotate(float duration, float target, EffectCurve Curve)
    { 
        return RotateTo(duration, target + transform.eulerAngles.z, Curve); 
    }
    public Effector RotateTo(float duration, float target)
    { 
        return Rotate(duration, target, increCurve); 
    }
    public Effector RotateTo(float duration, float target, EffectCurve Curve)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(RotateCoroutine(duration,target,Curve), duration));
        return this;
    }
    IEnumerator RotateCoroutine(float duration, float target, EffectCurve Curve)
    {
        float eTime = 0f;
        float originalRot = transform.eulerAngles.z;
        while (eTime <= duration)
        {
            transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(originalRot, target, Curve(eTime/duration)));
            eTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,0,target);
    }
#endregion

#region Color
    public Effector ColorChange(float duration, Color target)
    {
        return ColorChange(duration,target,increCurve);
    }
    public Effector ColorChange(float duration, Color target, EffectCurve Curve)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(ColorCoroutine(duration,target,Curve), duration));
        return this;
    }

    IEnumerator ColorCoroutine(float duration, Color target, EffectCurve Curve)
    {
        float eTime = 0f;
        Color c = spriteRenderer.color;
        Color originalColor = c;
        while (eTime <= duration)
        {
            c = Color.Lerp(originalColor, target, Curve(eTime/duration));
            spriteRenderer.color = c;
            eTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = target;
    }
    #endregion

#region Animation
    public Effector SimpleAnimation(float duration, Sprite[] sprites)
    {
#if UNITY_EDITOR

#endif
        QueueEffect(new Effect(SimpleAnimationCoroutine(duration, sprites), duration));
        return this;
    }

    IEnumerator SimpleAnimationCoroutine(float duration, Sprite[] sprites)
    {
        float eTime = 0f;
        Sprite originalSprite = spriteRenderer.sprite;
        while (eTime <= duration)
        {
            spriteRenderer.sprite = sprites[(int)(sprites.Length * (eTime / duration))];
            eTime += Time.deltaTime;
            yield return null;
        }
    }
    #endregion

#region Disable
    public Effector Disable(float timeOffset = 0f, bool destroy = false)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(DisableCoroutine(timeOffset,destroy), timeOffset));
        return this;
    }
    IEnumerator DisableCoroutine(float timeOffset, bool destroy)
    {
        if (timeOffset != 0f)
            yield return new WaitForSeconds(timeOffset);
        if (!destroy)
        {
            Stop(true);
            gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
        effectList.Clear();
        //isDoneSetting = false;
    }
    #endregion

#region Wait
    public Effector Wait(float duration)
    {
#if UNITY_EDITOR
#endif
        QueueEffect(new Effect(WaitCoroutine(duration), duration));
        return this;
    }
    IEnumerator WaitCoroutine(float timeOffset)
    {
        if (timeOffset != 0f)
            yield return new WaitForSeconds(timeOffset);
    }
    #endregion

#region Connections


    public Effector And()
    {
    #if UNITY_EDITOR
        if(effectList[effectList.Count-1].nextType != ChainType.DONE)
            Debug.LogWarning(gameObject.name + ": Chained Add after" + effectList[effectList.Count-1].nextType);
    #endif
        //if(!isPlaying || queueWhilePlay)
            effectList[effectList.Count-1].nextType = ChainType.AND;
        return this;
    }
    public Effector Then()
    {
#if UNITY_EDITOR
        if(effectList[effectList.Count-1].nextType != ChainType.DONE)
            Debug.LogWarning(gameObject.name + ": Chained Add after" + effectList[effectList.Count-1].nextType);
#endif
        //if (!isPlaying || queueWhilePlay)
            effectList[effectList.Count-1].nextType = ChainType.THEN;
        return this;
    }
    #endregion

    public void Stop(bool resetProperties = true)
    {
        if (isPlaying)
        {
            StopCoroutine(mainCoroutine);
            isPlaying = false;
        }
        if (resetProperties)
            resetProperty();
    }
    public void Play()
    {
    #if UNITY_EDITOR
        if(effectList[effectList.Count-1].nextType != ChainType.DONE)
                Debug.LogWarning(gameObject.name + ": Chain played after" + effectList[effectList.Count-1].nextType);
        if(effectList.Count == 0)
            Debug.LogWarning(gameObject.name + ": No effected attatched");

#endif
        if (isPlaying && cancelPlayingEffect)
        {
            Stop();
        }
        isPlaying = true;
        mainCoroutine = MainCoroutine();
        original_Pos = transform.position;
        original_Scale = transform.localScale;
        original_Roate = transform.rotation.eulerAngles.z;
        original_Color = spriteRenderer.color;
        original_Alpha = original_Color.a;
        StartCoroutine(mainCoroutine);
    }
    
    IEnumerator MainCoroutine()
    {
        int index = 0;
        List<Effect> effectBatch = new List<Effect>();
        List<Effect> effectListCopy = new List<Effect>();
        for(int i=0; i<effectList.Count; i++)
        {
            effectListCopy.Add(effectList[i]);
        }
        effectList.Clear();
        while (index < effectListCopy.Count)
        {
            effectBatch.Add(effectListCopy[index]);
            if(effectListCopy[index].nextType == ChainType.THEN || effectListCopy[index].nextType == ChainType.DONE)
            {
                effectBatch.Sort((x1, x2) => x1.duration.CompareTo(x2.duration));
                for(int i=0; i<effectBatch.Count-1; i++)
                    StartCoroutine(effectBatch[i].coroutine);
                yield return StartCoroutine(effectBatch[effectBatch.Count-1].coroutine);
                effectBatch.Clear();
            }
            index++;
        }
        isPlaying = false;
    }
    void QueueEffect(Effect effect)
    {
        //if (!isPlaying || queueWhilePlay)
            effectList.Add(effect);
    }
    void OnDisable()
    {
        resetProperty();
    }
    private void resetProperty()
    {
        transform.position = original_Pos;
        transform.localScale = original_Scale;
        transform.rotation = transform.rotation = Quaternion.Euler(0, 0, original_Roate);
        original_Color.a = original_Alpha;
        spriteRenderer.color = original_Color;
    }
    static float increCurve(float t)
    {
        return t;
    }
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
