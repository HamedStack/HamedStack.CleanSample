// ReSharper disable UnusedMember.Global

using MediatR;

namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class ActionExtensions
{
    public static Func<Unit> ToUnitFunc(this Action @this)
    {
        return () =>
        {
            @this();
            return new Unit();
        };
    }

    public static Func<T, Unit> ToUnitFunc<T>(this Action<T> @this)
    {
        return t =>
        {
            @this(t);
            return new Unit();
        };
    }

    public static Func<T1, T2, Unit> ToUnitFunc<T1, T2>(this Action<T1, T2> @this)
    {
        return (t1, t2) =>
        {
            @this(t1, t2);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, Unit> ToUnitFunc<T1, T2, T3>(this Action<T1, T2, T3> @this)
    {
        return (t1, t2, t3) =>
        {
            @this(t1, t2, t3);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, Unit> ToUnitFunc<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> @this)
    {
        return (t1, t2, t3, t4) =>
        {
            @this(t1, t2, t3, t4);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, Unit> ToUnitFunc<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> @this)
    {
        return (t1, t2, t3, t4, t5) =>
        {
            @this(t1, t2, t3, t4, t5);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6>(
        this Action<T1, T2, T3, T4, T5, T6> @this)
    {
        return (t1, t2, t3, t4, t5, t6) =>
        {
            @this(t1, t2, t3, t4, t5, t6);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6, T7>(
        this Action<T1, T2, T3, T4, T5, T6, T7> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6, T7, T8>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9,
        T10>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6, T7, T8,
        T9, T10, T11>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6, T7,
        T8, T9, T10, T11, T12>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Unit> ToUnitFunc<T1, T2, T3, T4, T5, T6,
        T7, T8, T9, T10, T11, T12, T13>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Unit> ToUnitFunc<T1, T2, T3, T4, T5,
        T6, T7, T8, T9, T10, T11, T12, T13, T14>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            return new Unit();
        };
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Unit> ToUnitFunc<T1, T2, T3,
        T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
        this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> @this)
    {
        return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) =>
        {
            @this(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
            return new Unit();
        };
    }
}