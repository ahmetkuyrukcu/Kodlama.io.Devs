﻿namespace Core.Persistence.Paging;

public static class Paginate
{
    public static IPaginate<T> Empty<T>()
    {
        return new Paginate<T>();
    }

    public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
    {
        return new Paginate<TSource, TResult>(source, converter);
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Reviewed.")]
public class Paginate<T> : IPaginate<T>
{
    public int From { get; set; }

    public int Index { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public int Pages { get; set; }

    public IList<T> Items { get; set; }

    public bool HasPrevious => Index - From > 0;

    public bool HasNext => Index - From + 1 < Pages;

    public Paginate()
    {
        Items = Array.Empty<T>();
    }

    public Paginate(IEnumerable<T> source, int index, int size, int from)
    {
        var enumerable = source as T[] ?? source.ToArray();

        if (from > index)
        {
            throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");
        }

        if (source is IQueryable<T> queryable)
        {
            Index = index;
            Size = size;
            From = from;
            Count = queryable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = queryable.Skip((Index - From) * Size).Take(Size).ToList();
        }
        else
        {
            Index = index;
            Size = size;
            From = from;
            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
        }
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Reviewed.")]
public class Paginate<TSource, TResult> : IPaginate<TResult>
{
    public int Index { get; }

    public int Size { get; }

    public int Count { get; }

    public int Pages { get; }

    public int From { get; }

    public IList<TResult> Items { get; }

    public bool HasPrevious => Index - From > 0;

    public bool HasNext => Index - From + 1 < Pages;

    public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
    {
        Index = source.Index;
        Size = source.Size;
        From = source.From;
        Count = source.Count;
        Pages = source.Pages;

        Items = new List<TResult>(converter(source.Items));
    }

    public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from)
    {
        var enumerable = source as TSource[] ?? source.ToArray();

        if (from > index)
        {
            throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");
        }

        if (source is IQueryable<TSource> queryable)
        {
            Index = index;
            Size = size;
            From = from;
            Count = queryable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = new List<TResult>(converter(queryable.Skip((Index - From) * Size).Take(Size).ToArray()));
        }
        else
        {
            Index = index;
            Size = size;
            From = from;
            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = new List<TResult>(converter(enumerable.Skip((Index - From) * Size).Take(Size).ToArray()));
        }
    }
}