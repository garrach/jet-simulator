using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JetInventory : MonoBehaviour
{
    private List<MissileItem> missileItems = new List<MissileItem>();
    public GameObject armor;
    public GameObject bomb;
    public float MisslelaunchSpeed;
    [System.Serializable]
    public class MissileItem
    {
        public readonly float missileSpeed=0.004f;
        public string missileName;
        public int missileID;
        public int missileLevel;
        public int missileCount;
        public int missileType;
        public int missileYield;

        public MissileItem(int sec, string name, int id)
        {
            missileName = name;
            missileID = id;
            missileYield = sec;
        }
    }

    public void AddMissile(MissileItem newMissile)
    {
        missileItems.Add(newMissile);
        Debug.Log("Added missile: " + newMissile.missileName + " to the jet's inventory.");
    }

    public void RemoveMissile(MissileItem missileToRemove)
    {
        if (missileItems.Contains(missileToRemove))
        {
            missileItems.Remove(missileToRemove);
            Debug.Log("Removed missile: " + missileToRemove.missileName + " from the jet's inventory.");
        }
        else
        {
            Debug.LogWarning("Missile not found in the jet's inventory.");
        }
    }

    public bool HasMissile(MissileItem missileToCheck)
    {
        return missileItems.Contains(missileToCheck);
    }

    void Start()
    {
        LoadAllMissiles();
    }

    void LoadAllMissiles()
    {
        // Add instances of MissileItem for each missile type
        AddMissile(new MissileItem(2,"AGM-65 Maverick", 1));
        AddMissile(new MissileItem(2,"AGM-114 Hellfire", 2));
        AddMissile(new MissileItem(2,"AIM-7 Sparrow", 3));
        AddMissile(new MissileItem(2,"AIM-9 Sidewinder", 4));
        AddMissile(new MissileItem(2,"GBU-12 Paveway II", 5));
        AddMissile(new MissileItem(2,"HJ-10", 6));
        AddMissile(new MissileItem(2,"JDAM (Version 1)", 7));
        AddMissile(new MissileItem(2,"JDAM (Version 2)", 8));
        AddMissile(new MissileItem(2,"KAB-500L", 9));
        AddMissile(new MissileItem(2,"Kh-29", 10));
        AddMissile(new MissileItem(2,"PL-11 (Version 1)", 11));
        AddMissile(new MissileItem(2,"PL-11 (Version 2)", 12));
        AddMissile(new MissileItem(2,"R-27 (Version 1)", 13));
        AddMissile(new MissileItem(2,"R-27 (Version 2)", 14));
        AddMissile(new MissileItem(2,"R-77", 15));
        AddMissile(new MissileItem(2,"TY-90", 16));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            launch();
        }
    }
    public void launch()
    {
        MissileItem missileToFire = missileItems[0];

        if (HasMissile(missileToFire))
        {
            StopAllCoroutines();
            StartCoroutine(missileEject(missileToFire.missileYield, missileToFire));
        }
        else
        {
            Debug.LogWarning("No missiles available in the jet's inventory.");
        }
    }

        void FireMissile(MissileItem missile)
        {
        GameObject missilePrefab = bomb;
            if (missilePrefab != null)
            {
                GameObject newMissile = Instantiate(missilePrefab, armor.transform.position,armor.transform.rotation);

                // Set the initial velocity or force for the missile
                Rigidbody missileRigidbody = newMissile.GetComponent<Rigidbody>();
                if (missileRigidbody != null)
                {
                    missileRigidbody.velocity = -newMissile.transform.forward * MisslelaunchSpeed * Time.deltaTime; 
                }
            }
        }
    IEnumerator missileEject(int sec,MissileItem missileItem)
    {
        Debug.Log("loading missile available in the jet's inventory.");
        yield return new WaitForSeconds(sec);       
        //RemoveMissile(missileItem);                
        FireMissile(missileItem); 
    }
}


