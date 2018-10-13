using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public event Action OnStartDeath;
    public event Action OnEndDeath;
    public event Action OnStartMove;
    public event Action OnEndMove;
    public event Action OnStartChase;
    public event Action OnEndChase;

    [Header("Control")]
    public SaveData masterSaveData;
    public SettingsData masterSettingsData;
    public GameOverScript gameOverController;
    public PauseScript pauseController;
    public WarningScript warningScreen;
    [Header("Animations")]
    public string standingAnimation;
    public string gunStandingAnimation;
    public string walkingAnimation;
    public string gunWalkingAnimation;
    public string deathAnimation;
    public GunControl gun;
    [Header("Movement")]
    public float moveSpeed = 15;
    [Header("Look")]
    public float minimumY = -90;
    public float maximumY = 90;
    public Camera mainCamera;
    public Camera secondCamera;
        
    private CharacterController local_Controller;
    private Animation local_Animation;
    private Vector2 cameraDir;
    private Vector2 lastCameraDir;
    private float rotateY = 0;
    private Vector3 movementDir;
    private Vector3 lastMovementDir;
    private bool rotateCamera = false;
    private bool moving = false;
    private bool dead = false;
    private bool paused = false;

    private void OnEnable()
    {
        if (pauseController != null) { pauseController.OnStartPause += OnStartPause; }
        if (pauseController != null) { pauseController.OnEndPause += OnEndPause; }
    }

    private void OnDisable()
    {
        if (pauseController != null) { pauseController.OnStartPause -= OnStartPause; }
        if (pauseController != null) { pauseController.OnEndPause -= OnEndPause; }
    }

    private void Start()
    {
        local_Controller = GetComponent<CharacterController>();
        local_Animation = GetComponent<Animation>();
        SetSecondCameraSmall();

        if (masterSaveData.hasGun) { ActivateGun(); }
        else { DeactivateGun(); }

    }

    private void Update()
    {
        if (dead || paused) { return;  }
        GetInput();
        if (moving) { Move(); }
        if (rotateCamera) { Look(); }
    }

    public void StartChase()
    {
        warningScreen.Activate();
        if (OnStartChase != null) { OnStartChase(); }
    }

    public void EndChase()
    {
        warningScreen.Deactivate();
        if (OnEndChase != null) { OnEndChase(); }
    }

    private void StartMove()
    {
        moving = true;
        local_Animation.Play(GetWalkingAnimation());
        if (OnStartMove != null) { OnStartMove(); }
    }

    private void EndMove()
    {
        moving = false;
        local_Animation.Play(GetStandingAnimation());
        if (OnEndMove != null) { OnEndMove(); }
    }

    private void GetInput()
    {

        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.z = Input.GetAxis("Vertical");
        cameraDir.x = Input.GetAxis("Mouse X");
        cameraDir.y = Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Cancel")) { PauseGame(); }
        if (movementDir.magnitude > 0 && lastMovementDir.magnitude == 0) { StartMove(); }
        if (movementDir.magnitude == 0 && lastMovementDir.magnitude > 0) { EndMove(); }
        if (cameraDir.magnitude > 0 && lastCameraDir.magnitude == 0) { rotateCamera = true; }
        if (cameraDir.magnitude == 0 && lastCameraDir.magnitude > 0) { rotateCamera = false; }

        lastMovementDir = movementDir;
        lastCameraDir = cameraDir;

    }
    
    private void Move()
    {
        Vector3 Movement = transform.TransformDirection(movementDir.normalized);
        local_Controller.SimpleMove(Movement * moveSpeed);
    }

    private void Look()
    {
        transform.eulerAngles += new Vector3(0, cameraDir.x * masterSettingsData.Sensitivity);
        rotateY += cameraDir.y * masterSettingsData.Sensitivity;
        rotateY = Mathf.Clamp(rotateY, minimumY, maximumY);
        mainCamera.transform.localEulerAngles = new Vector3(-rotateY, 0);
    }

    private void SetSecondCameraSmall()
    {
        secondCamera.rect = new Rect(0.8f, 0.7f, 0.2f, 0.3f);
    }

    private void SetSecondCameraBig()
    {
        secondCamera.rect = new Rect(0, 0, 1, 1);
    }

    public void PauseGame()
    {
        moving = false;
        dead = false;
        pauseController.PauseGame();
    }

    public void KillInSeconds(float attackDelay)
    {
        moving = false;
        dead = true;
        local_Animation.Play(GetStandingAnimation());
        SetSecondCameraBig();
        Invoke("Kill", attackDelay);
    }

    public void Kill()
    {
        moving = false;
        dead = true;
        Vector3 gunPosition = gun.transform.position;
        gunPosition.y = -0.2f;
        gun.transform.position = gunPosition;
        SetSecondCameraBig();
        warningScreen.Activate();
        local_Animation.Play(deathAnimation);
        if (OnStartDeath != null) { OnStartDeath(); }
        Invoke("DeathWait5Seconds", 5);
    }
    
    private void DeathWait5Seconds()
    {
        if (OnEndDeath != null) { OnEndDeath(); }
        gameOverController.GameOver();
    }

    public void FaceAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    public void ActivateGun()
    {
        masterSaveData.hasGun = true;
        gun.ActivateGun();
    }

    public void DeactivateGun()
    {
        masterSaveData.hasGun = false;
        gun.DeactivateGun();
    }

    private string GetStandingAnimation()
    {
        if (masterSaveData.hasGun) { return gunStandingAnimation; }
        else { return standingAnimation; }
    }

    private string GetWalkingAnimation()
    {
        if (masterSaveData.hasGun) { return gunWalkingAnimation; }
        else { return walkingAnimation; }
    }

    private void OnStartPause()
    {
        paused = true;
        gun.paused = true;
    }

    private void OnEndPause()
    {
        paused = false;
        gun.paused = false;
    }

}
