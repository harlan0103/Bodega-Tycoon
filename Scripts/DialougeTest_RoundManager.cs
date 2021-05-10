using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeTest_RoundManager : MonoBehaviour
{
    public static DialougeTest_RoundManager instance;

    public Text roundText;

    private int roundNum;

    public bool inTestMode = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (inTestMode)
        {
            DialougeManager.instance.InitializeDialogueManager();
            roundNum = 0;

            Debug.Log("In test round manager: round = " + roundNum);

            UpdateRoundUI();

            // Initialize all Npc
            NpcManager.instance.InitializeNpc();
            StoryManager.instance.InitializeStoryManager();

            // Initialize all story and sequnce as available
            StoryAssetsEditor storyAssets = Resources.Load<StoryAssetsEditor>("StoryAssets");
            foreach (StoryEntry story in storyAssets.storyList)
            {
                //Debug.Log(story.storyName);
                story.UpdateStoryProgress(StoryEntry.StoryProgress.AVAILABLE);

                foreach (StorySequence sequence in story.storySequences)
                {
                    sequence.UpdateSequenceProgress(StorySequence.StorySequenceProgress.AVAILABLE);
                }
            }
            //StoryManager.instance.OnNextRound(roundNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceToNextRound()
    {
        roundNum++;
        UpdateRoundUI();

        // StoryManager need to update as well
        StoryManager.instance.OnNextRound(roundNum);
        //NpcManager.instance.OnRoundUpdate();
    }

    private void UpdateRoundUI()
    {
        roundText.text = "Round: " + roundNum;

        //Debug.Log("Current round is: " + roundNum);
    }

    public int GetCurrentRound()
    {
        return roundNum;
    }
}
