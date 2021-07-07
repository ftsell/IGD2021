using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupP {
    public class KeyPressHandler : MonoBehaviour
    {

        private static KeyPressHandler _instance;
        public static KeyPressHandler instance { get { return _instance; }}
        
        private class PlayerStat {
            public GameObject player;
            public List<bool> hasHitNotes;
        }

        List<PlayerStat> playerStats = new List<PlayerStat>();

        List<Note> currentNotes = new List<Note>();

        void Awake() {
            if(_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }

        public void registerPlayer(GameObject player) {
            PlayerStat playerStat = new PlayerStat();
            playerStat.player = player;
            playerStat.hasHitNotes = new List<bool>();
            playerStats.Add(playerStat);
            Debug.Log(playerStats.Count);
        }

        public void registerNote(Note note) {
            foreach(var playerStat in playerStats) {
                playerStat.hasHitNotes.Add(false);
            }
            currentNotes.Add(note);
            foreach(var playerStat in playerStats) {
                playerStat.player.GetComponent<Player>().addNote(note);
            }
        }

        public void deregisterNote(Note note) {
            int index = currentNotes.IndexOf(note);
            //TODO call MissedHit() for players who havent hit the note
            foreach(var playerStat in playerStats) {
                if(!playerStat.hasHitNotes[index] && !note.bad) {
                    if(note.special) {
                        playerStat.player.GetComponent<Score>().SpecialMissed();
                    } else {
                        playerStat.player.GetComponent<Score>().Missed();
                    }
                }
                playerStat.hasHitNotes.RemoveAt(index);
                playerStat.player.GetComponent<Player>().removeNote(note);
            }
            currentNotes.Remove(note);
        
        }

        /**
        * Check all notes in note list for the given keytype.
        * If there is a matching note send the hitquaality to the player.
        * If there is no matching note---but the notelist is not empty---
        * then all notes in the list should be marked as being hit.
        * This is necessary to avoid key spamming by players.
        */
        public void keyPressed(GameObject currentPlayer, KeyType keyType) {
            PlayerStat currentPlayerStat = playerStats.Find(delegate (PlayerStat test){
                return test.player == currentPlayer;
            });

            if(currentNotes.Count == 0) { return; }

            bool foundMatchingNote = false;
            foreach( var note in currentNotes) {
                if(note.key == keyType) {
                    foundMatchingNote = true;
                    break;
                }
            }

            if(!foundMatchingNote) {
                foreach(var note in currentNotes) {
                    int index = currentNotes.IndexOf(note);
                    currentPlayerStat.hasHitNotes[index] = true;
                }
            }

            foreach (var note in currentNotes) {
                int index = currentNotes.IndexOf(note);
                if(!currentPlayerStat.hasHitNotes[index]) {
                    if(keyType == note.key) {
                        if(note.bad) {
                            currentPlayerStat.player.GetComponent<Score>().BadHit();
                            currentPlayerStat.player.GetComponent<Player_Dance>().BadKey();
                        } else if (note.special) {
                            currentPlayerStat.player.GetComponent<Score>().SpecialHit(note.hitQuality);
                            currentPlayerStat.player.GetComponent<Player_Dance>().SpecialKey();
                        }
                        else {
                            currentPlayerStat.player.GetComponent<Score>().Hit(note.hitQuality);
                        }

                        currentPlayerStat.hasHitNotes[index] = true;
                    }
                }
            }
            
        }
    }
}