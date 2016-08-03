﻿/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using System;
using net.r_eg.Conari.Exceptions;
using net.r_eg.Conari.WinAPI;

namespace net.r_eg.Conari.Core
{
    public abstract class Loader: ILoader, IDisposable
    {
        /// <summary>
        /// Active library.
        /// </summary>
        public Link Library
        {
            get;
            protected set;
        }

        /// <summary>
        /// Loads library into the address space.
        /// </summary>
        /// <param name="lib">The name of the library.</param>
        /// <returns></returns>
        protected bool load(string lib)
        {
            if(Library.IsActive) {
                throw new LoaderException($"The library '{Library.LibName}' should be unloaded before new loading '{lib}'.");
            }

            if(String.IsNullOrWhiteSpace(lib)) {
                throw new ArgumentException("The library name cannot be null or empty.", "lib");
            }

            Library = loadLibrary(lib);

            if(Library.Handle == IntPtr.Zero) {
                throw new LoadLibException($"Failed loading '{Library.LibName}': Check used architecture or existence of file.", true);
            }

            return true;
        }

        protected bool load()
        {
            return load(Library.LibName);
        }

        protected Link loadLibrary(string lib)
        {
            IntPtr hModule = NativeMethods.LoadLibraryEx(lib, IntPtr.Zero, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);
            return new Link(hModule, lib);
        }

        protected bool unload()
        {
            if(!Library.IsActive) {
                return true;
            }
            return NativeMethods.FreeLibrary(Library.Handle);
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // To correctly implement the disposable pattern. /CA1063
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            //...
            unload();
        }

        #endregion
    }
}