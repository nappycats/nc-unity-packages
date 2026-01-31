# ADR-001 — Component-less Typed Engine
We choose a delegate-driven, pooled engine. Each tween stores typed getter/setter delegates and a target value. The pool uses arrays for zero GC during ticks. The public surface remains tiny; improvements are hidden behind it.
