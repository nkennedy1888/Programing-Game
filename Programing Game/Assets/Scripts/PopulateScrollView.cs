using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PopulateScrollView : MonoBehaviour
{
    Dictionary<string, float> d_Rankings = new Dictionary<string, float>();
    public GameObject listing;
    GameObject newObj;
    public int numberOfListings;
    private Database localDB;
    // Start is called before the first frame update
    void Start()
    {
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void Populate(string name, float score)
    //{
    //        newObj = (GameObject)Instantiate(listing, transform);
    //        newObj.transform.Find("Name").GetComponent<Text>().text = name;
    //        newObj.transform.Find("Score").GetComponent<Text>().text = score + "%";
    //}

    public void GetRankings()
    {
        //Iterate over each user, collecting and calculating scores based on the following
        //(Questions correct/(Questions incorrect+Questions correct)) Note: will need to sum correct and incorrect from the three seperate difficulties
        //Beginner category has weight of 25%
        //Intermediate has weight of 30%
        //Advanced has weight of 45%
        //Multiply weighted avg by completion status
        float weightedAvg, begAvg, intAvg, advAvg, ranking;
        Debug.Log(localDB.users.Keys);
        foreach (KeyValuePair<string, UserData> entry in localDB.users)
        {
            if (entry.Key.Equals(""))
            {
                continue;
            }

            Debug.Log("Userentry in localDB.users: " + entry.Key + " " + entry.Value);
            //Reset all values
            weightedAvg = 0.0F;
            begAvg = 0.0F;
            intAvg = 0.0F;
            advAvg = 0.0F;
            ranking = 0.0F;
            //Calculate averages
            if (entry.Value.qstCrctBeginner == 0 && entry.Value.qstWrgBeginner == 0)
            {
                begAvg = 0;
            }
            else
            {
                begAvg = (entry.Value.qstCrctBeginner) / (entry.Value.qstWrgBeginner + entry.Value.qstCrctBeginner);

            }

            if (entry.Value.qstCrctIntermediate == 0 && entry.Value.qstWrgIntermediate == 0)
            {
                intAvg = 0;
            }
            else
            {
                intAvg = (entry.Value.qstCrctIntermediate) / (entry.Value.qstWrgIntermediate + entry.Value.qstCrctIntermediate);

            }

            if (entry.Value.qstCrctAdvanced == 0 && entry.Value.qstWrgAdvanced == 0)
            {
                advAvg = 0;
            }
            else
            {
                advAvg = (entry.Value.qstCrctAdvanced) / (entry.Value.qstWrgAdvanced + entry.Value.qstCrctAdvanced);

            }

            //Calculate weighted averages
            begAvg *= 0.25F;
            intAvg *= 0.30F;
            advAvg *= 0.45F;

            //Gives current 'grade'
            weightedAvg = begAvg + intAvg + advAvg;

            //Calculate final averages based on completion %
            begAvg *= entry.Value.progressBeginner;
            intAvg *= entry.Value.progressIntermediate;
            advAvg *= entry.Value.progressAdvanced;

            //Sum final avgs for ranking
            ranking = advAvg + intAvg + begAvg;
            
            d_Rankings.Add(entry.Key, ranking);
            
        }
        //l_Ranks = d_Rankings.ToList();
        //l_Ranks.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        //Should sort d_Rankings to display users in descending order based on score
        var sortedRankings = (from entry in d_Rankings orderby entry.Value descending select entry);

        

        foreach (KeyValuePair<string, float> entry in sortedRankings)
        {
            
            newObj = (GameObject)Instantiate(listing, transform);
            //this.transform.position + new Vector3(0, -40 * counter, 0)
            newObj.transform.SetParent(GameObject.FindGameObjectWithTag("ScrollView_Content").transform);
            newObj.transform.Find("Name").GetComponent<Text>().text = entry.Key;
            newObj.transform.Find("Score").GetComponent<Text>().text = entry.Value + "%";

            Debug.Log("Entry key and value: " + entry.Key + " " + entry.Value);

            newObj.SetActive(true);
        }

        //GameObject newObj; // Create GameObject instance

        //for (int i = 0; i < numberOfListings; i++)
        //{
        //    // Create new instances of our prefab until we've created as many as we specified
        //    newObj = (GameObject)Instantiate(listing, transform);

        //    // Randomize the color of our image
            
        //}
    }
}
