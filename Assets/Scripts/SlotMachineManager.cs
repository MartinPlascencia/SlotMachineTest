using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private SlotMachineData _slotMachineData;
    [Header("Assets")]
    [SerializeField]
    private Reel[] _reels;
    [Header("Controllers")]
    [SerializeField]
    private SpriteImagesController _spriteImagesController;
    [SerializeField]
    private GameDataController _gameDataController;
    
    private int _winnerSpinIndex = 0;

    private void Start()
    {
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
        StartCoroutine(SpinReelsCoroutine());
    }

    public void FinishSpin(){

        bool didWin = _gameDataController.SpinsData.Spins[_winnerSpinIndex].WinAmount > 0;
        if(didWin){
            int activeReels = _gameDataController.SpinsData.Spins[_winnerSpinIndex].ActiveReelCount;
            for (int i = 0; i < _reels.Length; i++)
            {
                if(i < activeReels)
                {
                    _reels[i].AnimateWinnerSprite();
                }
            }
        }
        
    }

    private IEnumerator SpinReelsCoroutine()
    {
        _winnerSpinIndex = Random.Range(0, _gameDataController.SpinsData.Spins.Length);
        //Debug.Log(_winnerSpinIndex + " _winnerSpinIndex");
        float spinDelay = 0;
        for (int i = 0; i < _reels.Length; i++)
        {
            yield return new WaitForSeconds(spinDelay);
            _reels[i].SpinReel(_gameDataController.SpinsData.Spins[_winnerSpinIndex].ReelIndex[i], _slotMachineData.SpinTime);
            spinDelay += _slotMachineData.DelayBetweenSpins;
        }

        yield return new WaitForSeconds(_slotMachineData.SpinTime);
        FinishSpin();
    }


}
