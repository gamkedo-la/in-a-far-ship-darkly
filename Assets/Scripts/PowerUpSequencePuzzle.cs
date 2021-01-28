using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSequencePuzzle : MonoBehaviour
{

    public List<PowerUp> powerUps = new List<PowerUp>();
    private List<PowerUp> activatedPowerUps = new List<PowerUp>();

    public bool puzzleIsCompleted{
        get{return isMatch;}
        set{;}
    }
    private bool isMatch;

    [SerializeField] PuzzleListener puzzleListener;

    
    void Start()
    {
        foreach (PowerUp powerUp in powerUps)
        {
            //reset activation on start
            if(powerUp.isPartOfSequence == false){ //if powerUp is not marked as part of a sequence, remove it from list
                powerUps.Remove(powerUp);
            }
            powerUp.isPowerUpActivated = false;
        }

    }

    public void HandlePowerUpActivated(PowerUp powerUp)
    {
        if(isMatch){return;} //if this puzzle is already completed, do nothing

        bool isInList = false;
        //check whether powerup is in the list

        for (int i = 0; i < powerUps.Count; i++){
            if(powerUps[i] == powerUp){
                activatedPowerUps.Add(powerUp);
                isInList = true;
            }
        }

        if(isInList){
            //check if the current order of activated powerups matches the correct order
            bool partialMatch = false;
            for (int i = 0; i < activatedPowerUps.Count; i++){
                if(activatedPowerUps[i] == powerUps[i]){
                    partialMatch = true;
                } else {
                    partialMatch = false;
                    ResetPowerUps(); //if the latest was not in the correct order, reset the whole puzzle
                    ResetActivatedList(); //clear list
                    break;
                }
            }

            if(activatedPowerUps.Count == powerUps.Count && partialMatch){
                isMatch = true;
                PuzzleCompleted();
            }
        }
    }

    private void PuzzleCompleted()
    {
        puzzleListener.SetPuzzleCompleted(true);
    }

    private void ResetActivatedList()
    {
        activatedPowerUps.Clear();
    }

    private void ResetPowerUps(){
        foreach (PowerUp p in activatedPowerUps)
        {
            p.isPowerUpActivated = false;
        }
    }

}