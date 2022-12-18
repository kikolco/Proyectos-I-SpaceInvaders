using UnityEngine;
 using System.Collections;
 
 public class ExplosionController: MonoBehaviour {
 
 private float explosionTime = 0.5f;
 
 void Update(){
 
 explosionTime -= Time.deltaTime;
 
 if (explosionTime <= 0.0f)
 {
    timerEnded();
 }
 
 }
 
 void timerEnded()
 {
    Destroy(this.gameObject);//destruye al enemigo
 }
 
 
 }