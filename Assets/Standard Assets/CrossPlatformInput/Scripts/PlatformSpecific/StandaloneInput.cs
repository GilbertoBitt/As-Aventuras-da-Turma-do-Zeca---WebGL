using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput.PlatformSpecific
{   

    public class StandaloneInput : VirtualInput
    {
        Dictionary<string, float> standAloneAxis = new Dictionary<string, float>();

        public void registerAxis(string name)
        {
            if (!standAloneAxis.ContainsKey(name))
            {
                standAloneAxis.Add(name, 0f);
            }
        }

        public override float GetAxis(string name, bool raw)
        {
            registerAxis(name);
            //return raw ? Input.GetAxisRaw(name) : Input.GetAxis(name);
            return standAloneAxis[name];
        }


        public override bool GetButton(string name)
        {
            return Input.GetButton(name);
        }


        public override bool GetButtonDown(string name)
        {
            return Input.GetButtonDown(name);
        }


        public override bool GetButtonUp(string name)
        {
            return Input.GetButtonUp(name);
        }


        public override void SetButtonDown(string name)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override void SetButtonUp(string name)
        {
            throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");
        }


        public override void SetAxisPositive(string name)
        {
            registerAxis(name);
            standAloneAxis[name] = 1f;
            /*throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");*/
        }


        public override void SetAxisNegative(string name)
        {
            registerAxis(name);
            standAloneAxis[name] = -1f;
            /*throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");*/
        }


        public override void SetAxisZero(string name)
        {
            registerAxis(name);
            standAloneAxis[name] = 0f;
            /*throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");*/
        }


        public override void SetAxis(string name, float value)
        {
            registerAxis(name);
            standAloneAxis[name] = value;
            /*throw new Exception(
                " This is not possible to be called for standalone input. Please check your platform and code where this is called");*/
        }


        public override Vector3 MousePosition()
        {
            return Input.mousePosition;
        }
    }
}