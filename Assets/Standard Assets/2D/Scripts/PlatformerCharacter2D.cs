using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
namespace UnityStandardAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour {
        [SerializeField] public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] public float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;
        public float currentSpeed;
        public float accelerationValue = 1f;
        public float flySpeed;
        public bool isFlying=false;
        public float[] speeds = new float[5];
        public int currentSpeedChosen = 0;
        // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        public Animator m_Anim;
        private Animation an_nim;   // Reference to the player's animator component.
        public Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public bool virarPerson;
        public bool hasFly = false;

        //public Vector2 vel;
        public int miniGame;
        public bool checkClickMove;
        public UnityEvent OnGrounded;
        public UnityEvent OnJump;
        public bool isVacine = false;
        public BoxCollider2D boxCollider2D;
        public CircleCollider2D circleCollider2D;
        public bool CheckQueda;
        private float OldXValue;
        public float timeCaindoOff;
        Animator personL;
        public UnityEvent OnEnableBtVacine;
        public UnityEvent OnEnableBtVacin2;
        public UnityEvent PararCorrer;
        public UnityEvent DidaticaVacine;
        public UnityEvent CameraZoomOutIVon;
        public UnityEvent stopFly;
        public UnityEvent stopSpeed;
        public UnityEvent EnableButton;
        public bool checkDeslizando;
        public GameObject collPePerson;
        public BoxCollider2D collPePersonC;
        public float _move;
        public bool isSpeedItem = false;
        public float speedItemSpeed = 7f;
        public bool EndedByLife;
        public Collider2D collVoando;
        public bool piscinaBolas;
       
        public GameObject piscina2;
        public UnityEvent PiscinaBolaIv;

        #region events

        public UnityEvent OnWalk;
        public UnityEvent OnVoltarCorrer;

        #endregion
        //public GameObject trigLimt;
        private void Awake() {

            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            circleCollider2D = GetComponent<CircleCollider2D>();
            m_Anim = GetComponent<Animator>();
            //an_nim = GetComponent<Animation>();
            if (virarPerson == false) {

                //GetComponent<Transform> ().localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);

            }

            m_Rigidbody2D = GetComponent<Rigidbody2D>();

        }
        public void Start() {

            personL = GetComponent<Animator>();
            if (collPePerson != null) {
                collPePersonC = collPePerson.GetComponent<BoxCollider2D>();
            }


        }

        private void FixedUpdate() {
            m_Grounded = false;

           
            // boxCollider2D.enabled = true;
            // collVoando.enabled = false;
            //vel = new Vector2(m_Rigidbody2D.velocity.x,m_Rigidbody2D.velocity.y);
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject) {
                    if (!isVacine) {
                        m_Grounded = true;
                    } else {
                        m_Grounded = true;
                        collVoando.enabled = false;
                      //  boxCollider2D.enabled = true;
                        OnGrounded.Invoke();
                    }
                }

            }

           

            m_Anim.SetBool("Ground", m_Grounded);
            if (isVacine == true) {
                m_Anim.SetBool("isFlying", hasFly);
                /*
                if (!m_Grounded) {
                    if (hasFly) {
                        boxCollider2D.enabled = false;
                    } 
                    else {
                        boxCollider2D.enabled = true;
                    }
                } 
                */
                // boxCollider2D.enabled = !hasFly;
            }
            if (m_Rigidbody2D.velocity.y > 0 && collPePerson != null) {
                //   Debug.Log("+");
                collPePerson.SetActive(false);
            } else if (m_Rigidbody2D.velocity.y < 0 && collPePerson != null) {
                //  Debug.Log("-");
                collPePerson.SetActive(true);
            }

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void ThisUpdate() {
            if (CheckQueda == false) {
                if (currentSpeed < speeds[currentSpeedChosen]) {
                    currentSpeed = Mathf.Lerp(currentSpeed, speeds[currentSpeedChosen], accelerationValue * Time.deltaTime);
                } else {
                    currentSpeed = Mathf.Lerp(currentSpeed, speeds[currentSpeedChosen], accelerationValue * Time.deltaTime);
                }
            }

        }

        public void Move(float move, bool crouch, bool jump) {
            _move = move;
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch")) {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl) {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));
                m_Anim.SetFloat("move", move);


                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                //if(miniGame==1) 

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight && virarPerson) {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight && virarPerson) {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground")) {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        public void Move2(float move, float vMove, bool crouch, bool jump) {
            int velocityX = Mathf.FloorToInt(m_Rigidbody2D.velocity.x);
            if (velocityX > 1) {
                OnWalk.Invoke();
            }

            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch")) {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
                    crouch = true;
                    //boxCollider2D.enabled =false;

                }
            }

            // Set whether or not the character is crouching in the animator
          
                m_Anim.SetBool("Crouch", crouch);
                boxCollider2D.enabled = !crouch;

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl) {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                //move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));
                m_Anim.SetFloat("move", move);

                // Move the character

                if (!isFlying) {
                    Physics2D.IgnoreLayerCollision(12, 0, false);
                    //  collPePerson.SetActive(true);
                    if (collPePersonC != null) {
                        collPePersonC.enabled = true;
                    }

                   // boxCollider2D.enabled = true;
                   // collVoando.enabled = false;
                    if (!isSpeedItem) {
                        m_Rigidbody2D.velocity = new Vector2(move * currentSpeed, m_Rigidbody2D.velocity.y);
                    } else {                        
                        m_Rigidbody2D.velocity = new Vector2(move * speedItemSpeed, m_Rigidbody2D.velocity.y);
                    }
                } else {
                    // Debug.Log("voando");
                      collPePerson.SetActive(false);
                    //boxCollider2D.enabled = false;
                    collVoando.enabled = true;

                    if (collPePersonC != null) {
                        collPePersonC.enabled = false;
                       
                    }
                    Physics2D.IgnoreLayerCollision(12, 0, true);
                   // m_Rigidbody2D.velocity = new Vector2(move * currentSpeed, vMove * flySpeed);
                    if (!isSpeedItem) {
                        m_Rigidbody2D.velocity = new Vector2(move * currentSpeed, vMove * flySpeed);
                    } else {
                        m_Rigidbody2D.velocity = new Vector2(move * speedItemSpeed, vMove * flySpeed);
                    }
                }

                //if(miniGame==1) 

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight && virarPerson) {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight && virarPerson) {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (jump) {
                // Add a vertical force to the player.
                OnJump.Invoke();
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }





        }



        void VoltarCorrer() {
            //   CheckCaindo(false);          
            if (!EndedByLife) {
            m_Anim.SetBool("Levantando", false);
            m_Anim.SetBool("BatendoCaixa", false);
            m_Anim.SetInteger("NumCaindo", 0);
            Invoke("EnableBTinfinty", 0.5f);
            OnEnableBtVacin2.Invoke();
        } else {
                
            }


        }

        void EnableBTinfinty() {

            if (checkDeslizando == true) {
                CrossPlatformInputManager.SetAxisNegative("Horizontal");
                checkDeslizando = false;
                OnVoltarCorrer.Invoke();
                m_Anim.SetBool("Levantando", false);
                m_Anim.SetBool("BatendoCaixa", false);
                m_Anim.SetBool("DelizandoLeite", false);
                m_Anim.SetBool("Crouch", false);
                m_Anim.SetInteger("NumCaindo", 0);
                m_Anim.SetBool("isFlying", false);
                collVoando.enabled = false;
            }
        }


        void CaindoFalse() {
            Invoke("Levantando", timeCaindoOff);
        }

        void EnableBtsInvinty() {
            OnEnableBtVacine.Invoke();
        }


        void Levantando() {

            m_Anim.SetBool("BatendoCaixa", false);
            if (!EndedByLife){
            m_Anim.SetBool("Levantando", true);
            }
            m_Anim.SetInteger("NumCaindo", 0);

           // EnableButton.Invoke();


        }
        private void Flip() {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            //m_Anim.spe =  theScale.x;

            //			an_nim.GetClip("RobotBoyRun").apparentSpeed=theScale.x;

            //m_Anim.speed{}

            transform.localScale = theScale;
        }
        void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.name == "Cool-Triguer") {
                checkClickMove = true;
            }
        }
       
        void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.name == "EndColl") {               
                stopFly.Invoke();
                stopSpeed.Invoke();
                Invoke("FimM", 1f);

            }
            

        }

        void OnCollisionEnter2D(Collision2D collision) {
            if (piscinaBolas && collision.gameObject.name == "BoxColliderPiscina1") {
                piscinaBolas = false;
                PiscinaBolaIv.Invoke();
            }

        }

        void FimM() {
            PararCorrer.Invoke();
            m_Anim.SetInteger("EndColl", 1);
            CameraZoomOutIVon.Invoke();
        }
        void EndVacine() {
            m_Anim.SetInteger("EndColl", 2);
        }
        void DidaticaVacineOn() {
            DidaticaVacine.Invoke();
        }
    }
}
