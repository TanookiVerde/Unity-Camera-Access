using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gallery : MonoBehaviour {
    [SerializeField] private Image photoImage;
    [SerializeField] private CanvasGroup canvasGroup;

    private Photo currentPhoto;

    public void TakePhoto(Photo photo)
    {
        currentPhoto = photo;
        photoImage.sprite = photo.GetAsSprite();
        canvasGroup.DOFade(1, 1f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void GiveUp()
    {
        canvasGroup.DOFade(0, 1f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
