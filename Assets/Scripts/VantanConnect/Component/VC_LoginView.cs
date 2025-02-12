using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace VTNConnect
{
    /// <summary>
    /// ログイン時のUI
    /// </summary>
    public class VC_LoginView : MonoBehaviour
    {
        [SerializeField] GameObject _qrRoot;
        [SerializeField] RawImage _outQRImage;
        [SerializeField] GameObject _connect;
        [SerializeField] Text _connectText;

        private void Start()
        {
            QRCodeSetup();
            _connect.SetActive(false);
        }

        public void QRCodeSetup()
        {
#if !AIGAME_IMPLEMENT
            _qrRoot.SetActive(VantanConnect.SystemSave.IsUseQRCode);
            if (VantanConnect.SystemSave.IsUseQRCode)
            {
                _outQRImage.texture = QRCodeMaker.BakeCode(VantanConnectQRString.MakeQRStringLinkage());
            }
#endif
        }

        public void SetEnable(bool isEnable)
        {
            this.gameObject.SetActive(isEnable);
        }

        public void Link(string displayName)
        {
            //TODO: 演出

            _connectText.text = "[CONNECT]" + displayName;
            _connect.SetActive(true);
        }
    }
}