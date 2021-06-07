using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GenerationOnMeshVertices : MonoBehaviour
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

    private void generateObj()
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
            float transPosZ = Random.Range(-transRange, transRange);
            Vector3 transPos = new Vector3(transPosX, transPosY, transPosZ);
            obj = Instantiate(generationObj, this.transform.position, Quaternion.identity);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = vertices[vertex_rand_i] + transPos;

            //回転は場合によりそうなので、以下を参考に書いてみてください。
            //Vector3 vec3 = new Vector3(obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y, obj.transform.position.z - this.transform.position.z);
            //obj.transform.localRotation = Quaternion.Euler(0, (Mathf.Atan(vec3.z / vec3.x) * Mathf.Rad2Deg)-90, 0);

            vertices.RemoveAt(vertex_rand_i);

            cntGeneration++;
            if (upperLimitOfGeneration < cntGeneration)
            {
                isGeneration = false;
                return;
            }
        }
    }

    private void setRandVals()
    {
        generationTime = Random.Range(minGenerationTime, maxGenerationTime);
        numOfGeneration = Random.Range(minNumOfGeneration, maxNumOfGeneration);
    }
}