﻿using Core.RegularExpressions.Algorithms;
using System.Diagnostics;

namespace Core.RegularExpressions.Nodes;

[DebuggerDisplay("Plus node [+]")]
public class PlusNode : RegexNode, IEquatable<PlusNode>
{
    public RegexNode Child { get; }

    public PlusNode(RegexNode child)
    {
        Child = child;
        Child.Parent = this;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
    public override R Accept<R>(IVisitor<R> visitor) => visitor.Visit(this);

    public override void Replace(RegexNode oldNode, RegexNode newNode) { }

    public bool Equals(PlusNode? other)
    {
        if (other is null)
            return false;

        return Id == other.Id &&
               Child.Equals(other.Child);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as PlusNode);
    }
    public override int GetHashCode()
    {
        // Combine hash codes of individual fields (from ChatGPT)
        int hash = 17;
        hash = hash * 23 + Id.GetHashCode();
        hash = hash * 23 + Child.GetHashCode();
        return hash;
    }
}
