﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

// This project defines a set of types that will be rewritten by the test cases to a new
// dll named "ClonedTestSubject".  The test cases then compare the final type information of both
// assemblies to confirm everything was re-written correction

// SEE RewriteTest in Lokad.ILPack.Tests


namespace TestSubject
{
    public partial class MyClass : IMyItf
    {
        [DllImport("DOES_NOT_EXIST")]
        public extern static int DontCall(int a);

        public void VoidMethod()
        {
        }

        public int IntMethod()
        {
            return 33;
        }

        public int IntMethodWithParameters(int a, int b)
        {
            return a + b;
        }

        public int AnotherParameterlessMethod()
        {
            // Argless methods must be rewritten using the next available param id
            // even if they have not parameters.
            return 0;
        }

        public int AnotherMethodWithParams(int a, int b)
        {
            return a + b;
        }

        public object AnotherMethodWithDifferentParameterTypes7(bool a, float b, int c0, int c1, int c2, object d,
            CancellationToken e)
        {
            return AnotherMethodWithDifferentParameterTypes3(a, c0 + c1 + c2 + (d != null ? (int) b : 0), e);
        }

        public object AnotherMethodWithDifferentParameterTypes3(bool a, int c, CancellationToken e)
        {
            return a ? c : (object) e;
        }

        public object AnotherMethodWithDefaultParameterValues(int a, string b = "Hallo, world!", string c = null, int d = 4711)
        {
            switch (a)
            {
                case 0:
                    return b;

                case 1:
                    return c;

                case 2:
                    return d;

                default:
                    throw new ArgumentException(nameof(a));
            }
        }
    }

    public class ClassWithProtectedMethod<T>
    {
        protected virtual void MyMethod(int foo)
        {
        }
    }

    public class ClassCallingProtectedMethod : ClassWithProtectedMethod<int>
    {
        protected override void MyMethod(int foo)
        {
            base.MyMethod(foo);
        }
    }
}