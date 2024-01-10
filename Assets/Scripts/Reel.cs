using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class Reel : MonoBehaviour
{
    [Header("Reel Settings")]
    [SerializeField]
    private float _tilesOffsetY = 122f;
    [SerializeField]
    private int _minimumSpacesToRound = 6;
    [Header("Winner Sprite Settings")]
    [SerializeField]
    private float _winnerSpriteScale = 1.5f;
    [SerializeField]
    private float _winnerScaleTime = 0.3f;
    private int _imageIndex = 0;
    private string[] _reelStrip;
    private SpriteImagesController _spriteImagesController;
    private Queue<Transform> _spinSpriteQueue = new Queue<Transform>();
    private float _lastSpritePositionY;
    private float _startPivot;
    private float _lastReelPositionY;
    private int _currentIndex;
    private Transform winnerSprite;
    public void InitializeReel(string[] reelStripsOrder, SpriteImagesController spriteImagesController)
    {
        int verticalPadding = transform.childCount / 2;
        _imageIndex = 0;
        _reelStrip = reelStripsOrder;
        _spriteImagesController = spriteImagesController;
        _startPivot = -_tilesOffsetY * verticalPadding;
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);

        for(int i = 0; i < transform.childCount; i++){
            Transform sprite = transform.GetChild(i);
            sprite.localPosition = new Vector3(sprite.localPosition.x, _startPivot + _tilesOffsetY * i, sprite.localPosition.z);
            _spriteImagesController.SetSpriteImage(_reelStrip[_imageIndex],sprite.GetComponent<Image>());
            _spinSpriteQueue.Enqueue(transform.GetChild(i));
            _lastSpritePositionY = sprite.localPosition.y;
            _imageIndex++;
        }

        _currentIndex = verticalPadding;

    }

    public void SpinReel(int winnerIndex, float spinTime)
    {
        //Debug.Log(winnerIndex + " winnerIndex " + _reelStrip[winnerIndex]);
        int numberOfSpaces;
        if(winnerIndex < _currentIndex){
            numberOfSpaces = _reelStrip.Length - _currentIndex + winnerIndex;
        }else if(winnerIndex - _currentIndex < _minimumSpacesToRound){
            numberOfSpaces = _reelStrip.Length - _currentIndex + winnerIndex;
        }else{
            numberOfSpaces = winnerIndex - _currentIndex;
        }
        _lastReelPositionY = transform.localPosition.y;
        transform.DOLocalMoveY(transform.localPosition.y -_tilesOffsetY * numberOfSpaces, spinTime).SetEase(Ease.InOutSine).OnUpdate(CheckSpritesPosition);
        _currentIndex = winnerIndex;
    }

    public void AnimateWinnerSprite()
    {
        winnerSprite.DOScale(_winnerSpriteScale, _winnerScaleTime).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopAnimatingWinnerSprite()
    {
        if(winnerSprite == null)
        {
            return;
        }
        winnerSprite.DOKill();
        winnerSprite.localScale = Vector3.one;
    }

    private void CheckSpritesPosition()
    {
        float distance = _lastReelPositionY - transform.localPosition.y;
        if(distance >= _tilesOffsetY){
            _lastReelPositionY-= _tilesOffsetY;
            Transform sprite = _spinSpriteQueue.Dequeue();
            _spinSpriteQueue.Enqueue(sprite);
            sprite.localPosition = new Vector3(sprite.localPosition.x, _lastSpritePositionY + _tilesOffsetY, sprite.localPosition.z);
            _spriteImagesController.SetSpriteImage(_reelStrip[_imageIndex],sprite.GetComponent<Image>());
            _lastSpritePositionY = sprite.localPosition.y;

            if(_imageIndex == _currentIndex)
            {
                winnerSprite = sprite;
            }
            _imageIndex++;
            if(_imageIndex >= _reelStrip.Length)
            {
                _imageIndex = 0; 
            }
        }
    }
        
}



