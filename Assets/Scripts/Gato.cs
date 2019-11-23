﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gato : MonoBehaviour {


    public NavMeshAgent agent;
    protected Mundo mundo;
    private float walkingSpeed = 1f;
    private float runningSpeed = 2f;
    private bool estaSentado = false;
    private Animator animator;

    void Awake()
    {
        mundo = FindObjectOfType<Mundo>();
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
    }

    public bool isInPosition()
    {

        return (agent.remainingDistance < 0.2f);
    }

    //Double-check
    public bool distance(Vector3 target)
    {
        return Vector3.Distance(transform.position, target) <= 0.8;
    }

	public void walkTo(Vector3 destination){
        agent.isStopped = false;
        agent.speed = walkingSpeed;
        animator.SetFloat("Animation", 0);
        agent.destination = destination;
        //rotateTowards(destination);
    }

    public void setAxis()
    {
        animator.SetFloat("Horizontal", transform.right.x);
        animator.SetFloat("Vertical", transform.right.z);
    }

    public void runTo(Vector3 destination){
        agent.isStopped = false;
        agent.speed = runningSpeed;
        animator.SetFloat("Animation", 1);
        agent.SetDestination(destination);

    }

    protected void pick(GameObject plato){
        //Animación de coger
        animator.SetFloat("Animation", 9);
        Destroy(plato, 0);
    }


    //Animación inversa a pick
    protected void set(string plato) {
        animator.SetFloat("Animation", 9);
        Instantiate(mundo.plato);
    }

    protected void idle(){
        agent.isStopped = true;
        animator.SetFloat("Animation", 3);
    }

    //Se podría cambiar como unico del cliente que es el unico que se sienta
    protected void sitDown(Transform lookAt){
        agent.isStopped = true;
        animator.SetFloat("Animation", 4);
        estaSentado = true;
    }

    protected void rotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4f);
    }

    protected bool isLookingTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        float abs = Mathf.Abs(Quaternion.Dot(transform.rotation, lookRotation));
        return (abs >= 0.9999999f);
    }

    protected void getUp(){
        //animacion
        estaSentado = false;
    }

    protected void wait() {
        agent.isStopped = true;
        animator.SetFloat("Animation", 2);
    }

    protected void eat()
    {
        agent.isStopped = true;
        animator.SetFloat("Animation", 6);
    }

    protected void cook(/*Item food*/){}

    protected void play(){
        agent.isStopped = true;
        animator.SetFloat("Animation", 2);
    }

    protected void angry(Vector3 lookAt){}

    protected void shamed(Vector3 lookAt){}

    void FixedUpdate()
    {
        setAxis();
    }
}
