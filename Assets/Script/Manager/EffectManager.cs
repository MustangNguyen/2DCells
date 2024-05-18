using System.Collections;
using System.Collections.Generic;
using static GameStatic;
using UnityEngine;
using Lean.Pool;
using TMPro;

public class EffectManager : Singleton<EffectManager>
{
    public Color damage = Color.red;
    public Color heal = Color.green;
    public StatusEffect statusEffect;
    public GameObject effectHolder;
    public void ShowDamageInfict(int damage, int criticalTier, Transform transform)
    {
        Color orange = new Color(1f, 0.5f, 0f, 1f);
        Vector2 temp = new Vector2(transform.position.x, transform.position.y);
        temp = new Vector2(transform.position.x + Random.Range(-0.5f, 0.51f), transform.position.y);
        StatusEffect statusEffect = LeanPool.Spawn(this.statusEffect, temp, Quaternion.identity,effectHolder.transform);
        statusEffect.statusText.text = damage.ToString();
        switch (criticalTier)
        {
            case 0:
                statusEffect.statusText.color = CRITICAL_TIER_0_COLOR;
                break;
            case 1:
                statusEffect.statusText.color = CRITICAL_TIER_1_COLOR;
                break;
            case 2:
                statusEffect.statusText.color = CRITICAL_TIER_2_COLOR;
                break;
            case 3:
                statusEffect.statusText.color = CRITICAL_TIER_3_COLOR;
                break;
            case 4:
                statusEffect.statusText.color = CRITICAL_TIER_4_COLOR;
                break;
            default:
                statusEffect.statusText.color = CRITICAL_TIER_5_COLOR;
                break;
        }
    }
    public void TransformStringByRandom(TextMeshProUGUI inputString, string outputString, float time){
        StartCoroutine(IETransformStringByRandom(inputString,outputString,time));
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
                else if((int)charArray[i]<91)
                    charArray[i] = (char)Random.Range(65, 91);
                else
                    charArray[i]= (char)Random.Range(97,123);
            }
            time -= Time.fixedDeltaTime;
            inputString.text = string.Concat(charArray);
        }
        inputString.text = outputString;
    }
}
