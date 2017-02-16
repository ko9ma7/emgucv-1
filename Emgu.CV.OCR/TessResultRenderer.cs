﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2017 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------


using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Diagnostics;

namespace Emgu.CV.OCR
{
    public interface ITessResultRenderer
    {
        IntPtr TessResultRendererPtr { get; }
    }

    public class PDFRenderer : UnmanagedObject, ITessResultRenderer
    {
        private IntPtr _tessResultRendererPtr;

        public PDFRenderer(String outputBase, String dataDir, bool textOnly)
        {
            using (CvString csOutputBase = new CvString(outputBase))
            using (CvString csDataDir = new CvString(dataDir))
            {
                _ptr = OcrInvoke.TessPDFRendererCreate(csOutputBase, csDataDir, textOnly, ref _tessResultRendererPtr);
            }
        }

        protected override void DisposeObject()
        {
            if (IntPtr.Zero != _ptr)
                OcrInvoke.TessPDFRendererRelease(ref _ptr);
        }

        public IntPtr TessResultRendererPtr
        {
            get
            {
                return _tessResultRendererPtr;
            }
        }
    }

    public static partial class OcrInvoke
    {
        [DllImport(CvInvoke.ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
        internal static extern IntPtr TessPDFRendererCreate(
            IntPtr outputbase, 
            IntPtr datadir, 
            [MarshalAs(CvInvoke.BoolMarshalType)]
            bool textonly, 
            ref IntPtr resultRenderer);

        [DllImport(CvInvoke.ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
        internal static extern void TessPDFRendererRelease(ref IntPtr renderer);
    }
}