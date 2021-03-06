using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

  public Mesh mesh;
  public Material material;
  public int maxDepth = 100;
  public float childScale, wait;
  private float positionAlpha = 0f, positionDir = 0f;
  private bool positionUp = true, master;
  private int depth;
  private Vector3 initLocalPosition, otherLocalPosition;

  void Start() {
    gameObject.AddComponent<MeshFilter>().mesh = mesh;
    gameObject.AddComponent<MeshRenderer>().material = material;
    if(depth < maxDepth) {
      StartCoroutine(CreateChildren());   
    }
  }

  private IEnumerator CreateChildren() {
    yield return new WaitForSeconds(0.2f);
    //new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.back);
    new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.forward, Quaternion.identity);
    yield return new WaitForSeconds(0.2f);
    new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, 90f));
    yield return new WaitForSeconds(0.2f);
    new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.left,  Quaternion.Euler(0f, 0f, 90f));
  }

  private void Initialize(Fractal parent, Vector3 dir, Quaternion orientation) {
    mesh = parent.mesh;
    material = parent.material;
    maxDepth = parent.maxDepth;
    depth = parent.depth + 1;
    transform.parent = parent.transform;
    childScale = parent.childScale;

    transform.localScale = Vector3.one * parent.childScale;

    transform.localPosition = dir * 1.2f;
    transform.localRotation = orientation;

    initLocalPosition = transform.localPosition;
    otherLocalPosition = initLocalPosition + dir * childScale * 0.2f;
  }

  void Update() {
    transform.localPosition = Vector3.Lerp(initLocalPosition, otherLocalPosition, positionAlpha);

    if(positionAlpha > 1f)  positionAlpha = 1f;
    if(positionAlpha < 0f)  positionAlpha = 0f;

    if (positionUp) positionDir += Time.deltaTime * 0.1f;
    else            positionDir -= Time.deltaTime * 0.1f;
    
    if (positionAlpha > 0.5f) positionUp = false;
    else                      positionUp = true;

    positionAlpha += positionDir;

    if(depth == 0) {
      transform.Rotate(0f, 0f, 5f);
    }
  }

}