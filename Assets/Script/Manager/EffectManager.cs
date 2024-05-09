using System.Collections;
using System.Collections.Generic;
using static GameStatic;
using UnityEngine;
using Lean.Pool;

public class EffectManager : Singleton<EffectManager>
{
    public Color damage = Color.red;
    public Color heal = Color.green;
    public StatusEffect statusEffect;
    public Transform effectHolder;
    public void ShowDamageInfict(int damage, int criticalLevel, Transform transform)
    {
        Color orange = new Color(1f, 0.5f, 0f, 1f);
        Vector2 temp = new Vector2(transform.position.x, transform.position.y);
        temp = new Vector2(transform.position.x + Random.Range(-0.5f, 0.51f), transform.position.y);
        StatusEffect statusEffect = LeanPool.Spawn(this.statusEffect, temp, Quaternion.identity,effectHolder);
        statusEffect.statusText.text = damage.ToString();
        switch (criticalLevel)
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
}
