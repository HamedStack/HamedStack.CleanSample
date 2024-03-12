// ReSharper disable UnusedMember.Global

namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class ApplyExtensions
{
    public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 t1)
    {
        return t2 => func(t1, t2);
    }

    public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 t1)
    {
        return (t2, t3) => func(t1, t2, t3);
    }

    public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func,
        T1 t1)
    {
        return (t2, t3, t4) => func(t1, t2, t3, t4);
    }

    public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(
        this Func<T1, T2, T3, T4, T5, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5) => func(t1, t2, t3, t4, t5);
    }

    public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6) => func(t1, t2, t3, t4, t5, t6);
    }

    public static Func<T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7) => func(t1, t2, t3, t4, t5, t6, t7);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8) => func(t1, t2, t3, t4, t5, t6, t7, t8);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9) => func(t1, t2, t3, t4, t5, t6, t7, t8, t9);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,
        TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10) => func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,
        T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9,
        T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) =>
            func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Apply<T1, T2, T3, T4, T5, T6, T7,
        T8, T9, T10, T11, T12, T13, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) =>
            func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Apply<T1, T2, T3, T4, T5, T6,
        T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) =>
            func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Apply<T1, T2, T3, T4, T5,
        T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) =>
            func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
    }

    public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Apply<T1, T2, T3, T4,
        T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, T1 t1)
    {
        return (t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) =>
            func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
    }
}