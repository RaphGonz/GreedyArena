using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] prefabs;
    public Vector2[] offset;

    private Weapon[] weaponComponents;

    private GameObject[] currentWeapons;
    private Vector2 _shootingDirection;

    [SerializeField] private GameObject player;

    int iteration = 1;

    

    void Start()
    {
        player = GameObject.Find("Player");

        currentWeapons = new GameObject[prefabs.Length];
        weaponComponents = new Weapon[prefabs.Length];

        //Todo : seuls les armes ramassées par le racoon doivent être instanciées.

        for (int i = 0; i < prefabs.Length; i++)
        {
            currentWeapons[i] = Instantiate(prefabs[i], transform.position + new Vector3(offset[i].x, offset[i].y), prefabs[i].transform.rotation);
            currentWeapons[i].transform.parent = gameObject.transform;
            weaponComponents[i] = currentWeapons[i].GetComponent<Weapon>();
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
