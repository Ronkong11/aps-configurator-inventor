/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Autodesk Design Automation team for Inventor
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

using Microsoft.Extensions.Logging;
using Shared;
using System.Threading.Tasks;

namespace WebApplication.Job
{
    public abstract class JobItemBaseBase
    {
        public abstract string Id { get; }
        public abstract InventorParameters Parameters { get; private set; }
        protected abstract ILogger Logger { get; }

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
        public abstract override async Task ProcessJobAsync(IResultSender resultSender);
    }
}