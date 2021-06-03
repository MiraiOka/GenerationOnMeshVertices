using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GenerationOnMeshVertex : MonoBehaviour
{
    [SerializeField] GameObject[] generationObjs;
    [SerializeField] int[] generationObjRatios;
    [SerializeField] float transRange;
    [SerializeField] int numOfInitGeneration;
    [SerializeField] float minGenerationTime;
    [SerializeField] float maxGenerationTime;
    [SerializeField] float minNumOfGeneration;
    [SerializeField] float maxNumOfGeneration;
    [SerializeField] int upperLimitOfGeneration;

    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    float time = 0;
    float generationTime = 0;
    float numOfGeneration = 0;
    GameObject obj;
    int totalGenerationObjRatio = 0;
    int cntGeneration = 0;
    bool isGeneration = false;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices.AddRange(mesh.vertices.Distinct());

        foreach (int gpr in generationObjRatios)
        {
            totalGenerationObjRatio += gpr;
        }

        numOfGeneration = numOfInitGeneration;
        generateObj();
        setRandVals();
    }

    private void Update()
    {
        if (isGeneration)
        {
            time += Time.deltaTime;
            if (time > generationTime)
            {
                generateObj();
                setRandVals();
                time = 0;
            }
        }
    }

    void generateObj()
    {
        for (int i = 0; i < numOfGeneration; i++)
        {
            //どのPrefabをInstantiateするかを決めるための乱数
            int randPrefabVal = Random.Range(0, totalGenerationObjRatio + 1);

            //どのPrefabをInstantiateするかを判断するために使用する変数
            int jadgmentPrefabVal = 0;
            GameObject generationObj = generationObjs[0];
            for (int ratios_i = 0; ratios_i < generationObjRatios.Length; ratios_i++)
            {
                jadgmentPrefabVal += generationObjRatios[ratios_i];
                if (randPrefabVal <= jadgmentPrefabVal)
                {
                    generationObj = generationObjs[ratios_i].gameObject;
                    break;
                }
            }

            int vertex_rand_i = Random.Range(0, vertices.Count);
            float transPosX = Random.Range(-transRange, transRange);
            float transPosY = Random.Range(-transRange, transRange);
            float transPosZ = Random.Range(-transRange, 0);
            Vector3 transPos = new Vector3(transPosX, transPosY, transPosZ);
            obj = Instantiate(generationObj, this.transform.position, Quaternion.identity);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = vertices[vertex_rand_i] + transPos;
            Vector2 vec2 = new Vector2(obj.transform.position.x - this.transform.position.x, (obj.transform.position.z - this.transform.position.z) * 20f);
            obj.transform.localRotation = Quaternion.Euler(0, (-Mathf.Atan(vec2.y / vec2.x) * Mathf.Rad2Deg) + 90, Random.value * 360);
            vertices.RemoveAt(vertex_rand_i);

            cntGeneration++;
            if (upperLimitOfGeneration < cntGeneration)
            {
                isGeneration = false;
                Debug.Log(this.gameObject.name + " is fin");
                return;
            }
        }
    }

    private void setRandVals()
    {
        generationTime = Random.Range(minGenerationTime, maxGenerationTime);
        numOfGeneration = Random.Range(minNumOfGeneration, maxNumOfGeneration);
    }

    public void init()
    {

    }
}