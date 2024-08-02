using System.Collections;
using System.Collections.Generic;
using static GameStatic;
using UnityEngine;
using Lean.Pool;
using TMPro;
using Cinemachine;
using System.Linq;
using Unity.Collections;
using System;

public class GeneralEffectManager : Singleton<GeneralEffectManager>
{
    public void TransformStringByRandom(TextMeshProUGUI inputString, string outputString, float time)
    {
        StartCoroutine(IETransformStringByRandom(inputString, outputString, time));
    }
    public IEnumerator IETransformStringByRandom(TextMeshProUGUI inputString, string outputString, float time)
    {
        char[] charArray = inputString.text.ToCharArray();
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < charArray.Length; i++)
            {
                if ((int)charArray[i] == 32) continue;
                else if ((int)charArray[i] < 91)
                    charArray[i] = (char)UnityEngine.Random.Range(65, 91);
                else
                    charArray[i] = (char)UnityEngine.Random.Range(97, 123);
            }
            time -= Time.fixedDeltaTime;
            inputString.text = string.Concat(charArray);
        }
        inputString.text = outputString;
    }
    public void MoveXDurationYParabolaSpeed(RectTransform start, Vector3 finish, float duration)
    {
        Vector3 lastRectPosition = new Vector3(start.anchoredPosition.x, start.anchoredPosition.y);
        duration *= 2;
        StartCoroutine(IEParabolaMoveRect());
        IEnumerator IEParabolaMoveRect()
        {
            float timeElapse = 0f;
            while (timeElapse < duration / 2)
            {
                start.anchoredPosition = new Vector3(lastRectPosition.x + (finish.x * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / duration) - 1, 2))), lastRectPosition.y + (finish.y * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / duration) - 1, 2))), 0);
                timeElapse += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);

            }
            start.anchoredPosition = new Vector2(lastRectPosition.x + finish.x, lastRectPosition.y + finish.y);
        }
    }
    /// <summary>
    /// Resize UI elements by add vector3 finish to original size by duration second
    /// </summary>
    /// <param name="start">input rectransform</param>
    /// <param name="finish">add to original size</param>
    /// <param name="duration">float</param>
    /// <param name="isRevertSpeed">the direction of parabol false: go down, true: go up</param>
    public void RectXDurationYParabolaSpeed(RectTransform start, Vector3 finish, float duration, bool isRevertSpeed = false,Action onFinish = null)
    {
        Vector2 lastRect = new Vector2(start.sizeDelta.x, start.sizeDelta.y);
        duration *= 2;
        StartCoroutine(IEParabolaMoveRect());
        IEnumerator IEParabolaMoveRect()
        {
            if (!isRevertSpeed)
            {
                float timeElapse = 0f;
                while (timeElapse < duration / 2)
                {
                    start.sizeDelta = new Vector2(lastRect.x + (finish.x * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / duration) - 1, 2))), lastRect.y + (finish.y * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / duration) - 1, 2))));
                    timeElapse += Time.deltaTime;
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                start.sizeDelta = new Vector2(lastRect.x + finish.x, lastRect.y + finish.y);
            }
            else
            {
                float timeElapse = duration / 2;
                while (timeElapse < duration)
                {
                    start.sizeDelta = new Vector2(lastRect.x + (finish.x * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / duration) - 1, 2))), lastRect.y + (finish.y * (1 - Mathf.Pow(2 * Mathf.Clamp01(timeElapse / duration) - 1, 2))));
                    timeElapse += Time.deltaTime;
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                start.sizeDelta = new Vector2(lastRect.x + finish.x, lastRect.y + finish.y);
            }
            onFinish?.Invoke();
        }
    }
}