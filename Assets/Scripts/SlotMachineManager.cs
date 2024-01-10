using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotMachineManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private SlotMachineData _slotMachineData;
    [Header("Assets")]
    [SerializeField]
    private Reel[] _reels;
    [SerializeField]
    private GameState _gameState;
    [Header("Controllers")]
    [SerializeField]
    private SpriteImagesController _spriteImagesController;
    [SerializeField]
    private GameDataController _gameDataController;
    [SerializeField]
    private ResourcesController _resourcesController;
    [Header("Events")]
    [SerializeField]
    private UnityEvent OnSpin;
    [SerializeField]
    private UnityEvent OnWin;
    [SerializeField]
    private UnityEvent OnLose;
    [SerializeField]
    private UnityEvent NotEnoughGold;
    
    private int _winnerSpinIndex = 0;

    private void Start()
    {
        _gameState.GamePlayingState = GameState.State.WaitingForSpin;
        _resourcesController.InitializeGold();
        _gameDataController.GetGameData();
        _spriteImagesController.InitializeImages();
        InitializeReels();

    }

    private void InitializeReels(){
        for(int i = 0; i < _reels.Length; i++){
            _reels[i].InitializeReel(_gameDataController.ReelStripsData.ReelStrips[i], _spriteImagesController);
        }
    }

    public void SpinReels(){
        _resourcesController.SetWinAmount(0);
        StopReelsAnimation();
        if(_gameState.GamePlayingState == GameState.State.Spinning){
            Debug.LogWarning("Already spinning");
            return;
        }
        if(!_resourcesController.HasEnoughGoldForSpin()){
            Debug.LogWarning("Not enough gold for spin");
            return;
        }
        OnSpin?.Invoke();
        StartCoroutine(SpinReelsCoroutine());
        _gameState.GamePlayingState = GameState.State.Spinning;
    }

    private void StopReelsAnimation(){
        for (int i = 0; i < _reels.Length; i++)
        {
            _reels[i].StopAnimatingWinnerSprite();
        }
    }

    private void AnimateWinnerReels(int activeReels){
        for (int i = 0; i < _reels.Length; i++)
        {
            if(i < activeReels)
            {
                _reels[i].AnimateWinnerSprite();
            }
        }
    }

    public void FinishSpin(){

        int winAmount = _gameDataController.SpinsData.Spins[_winnerSpinIndex].WinAmount;
        if(winAmount > 0){
            OnWin?.Invoke();
            AnimateWinnerReels(_gameDataController.SpinsData.Spins[_winnerSpinIndex].ActiveReelCount);
            _resourcesController.SetWinAmount(winAmount);
            _resourcesController.AddGold(winAmount);
        }else{
            OnLose?.Invoke();
        }
        _gameState.GamePlayingState = GameState.State.WaitingForSpin;
        
    }

    private IEnumerator SpinReelsCoroutine()
    {
        _winnerSpinIndex = UnityEngine.Random.Range(0, _gameDataController.SpinsData.Spins.Length);
        Spin winnerSpin = _gameDataController.SpinsData.Spins[_winnerSpinIndex];
        //Debug.Log(winnerSpin.WinAmount + " winAmount " + winnerSpin.ActiveReelCount + " activeReelCount");
        float spinDelay = 0;
        for (int i = 0; i < _reels.Length; i++)
        {
            yield return new WaitForSeconds(spinDelay);
            _reels[i].StopAnimatingWinnerSprite();
            _reels[i].SpinReel(winnerSpin.ReelIndex[i], _slotMachineData.SpinTime);
            spinDelay += _slotMachineData.DelayBetweenSpins;
        }

        yield return new WaitForSeconds(_slotMachineData.SpinTime);
        FinishSpin();
    }


}
