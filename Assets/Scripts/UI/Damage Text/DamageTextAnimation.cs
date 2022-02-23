using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace RPG.UI.DamageText
{
    public class DamageTextAnimation : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup = null;
        [SerializeField] TextMeshProUGUI text = null;

        Sequence mySequence;

        void Start()
        {
            mySequence = DOTween.Sequence();
            Vector3 endMoveValue = new Vector3(Random.Range(-1000, 1000f), Random.Range(300f, 600f), Random.Range(-1000f, 1000f));
            mySequence.Append(text.transform.DOLocalMove(endMoveValue, 0.5f));
            mySequence.Append(canvasGroup.DOFade(0, 0.7f));
            Vector3 endScaleValue = new Vector3(1.3f, 1.3f, 1.3f);
            mySequence.Insert(0, text.transform.DOScale(endScaleValue, 0.5f));
            mySequence.AppendCallback(() => Destroy(gameObject));
        }
    }

}