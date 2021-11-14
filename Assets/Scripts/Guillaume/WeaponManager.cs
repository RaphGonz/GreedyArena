using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] prefabs;
    public Vector2[] offset;

    private Weapon[] weaponComponents;

    private GameObject[] currentWeapons;
    private Vector2 _shootingDirection;

    private RacoonManager racoonManager;

    [SerializeField] private GameObject player;




    

    void Start()
    {
        player = GameObject.Find("Player");
        racoonManager = GameObject.Find("Racoon").GetComponent<RacoonManager>();

        bool[] weaponsAvailable = racoonManager.weaponTypeList;
        int N = weaponsAvailable.Where(c => c).Count();
        Debug.Log(weaponsAvailable.Length);


        currentWeapons = new GameObject[N];
        weaponComponents = new Weapon[N];

        //Todo : seuls les armes ramassées par le racoon doivent être instanciées.

        int j = 0;

        for (int i = 0; i < prefabs.Length; i++)
        {
            if (weaponsAvailable[i])
            {
                currentWeapons[j] = Instantiate(prefabs[i], transform.position + new Vector3(offset[i].x, offset[i].y), prefabs[i].transform.rotation);
                currentWeapons[j].transform.parent = gameObject.transform;
                weaponComponents[j] = currentWeapons[j].GetComponent<Weapon>();
                j++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < currentWeapons.Length; i++)
        {
            _shootingDirection = player.transform.position - currentWeapons[i].transform.position;
            weaponComponents[i].Shoot(_shootingDirection);
        }
    }


}
