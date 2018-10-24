## Explicit status ##
[![Build status](https://rickpowell.visualstudio.com/ExplicitStatus/_apis/build/status/ExplicitStatus-CI)](https://rickpowell.visualstudio.com/ExplicitStatus/_build/latest?definitionId=3)

Explicit status allow an explicit composition of a status (usually an enum). This is useful (for example) when the addition of members to a type can evolve how a status should be calculated.

Consider the following:

```
public class MyType
{
    public string RequiredField { get; set; }

    public MyStatus Status => string.IsNullOrWhiteSpace(RequiredField) 
        ? Incomplete
        : Complete
}

public enum MyStatus
{
    Incomplete,
    Complete
}
```

When a new field is added:

```
public class MyType
{
    public string RequiredField { get; set; }

    public int? AnotherRequiredField { get; set; } // new

    public MyStatus Status => string.IsNullOrWhiteSpace(RequiredField) 
        ? Incomplete
        : Complete
}
```

the status is still Complete!

### Quick start

```
public class MyType
{
    private readonly IStatus<MyType, MyStatus> _myStatus;

    public MyStatus => _myStatus.Get(this);

    public MyType()
    {
        _myStatus = StatusBuilder
            .BuildStatus<Status>()
            .FromType<MyType>()
            .IsStatus(MyStatus.Incomplete).When(x => string.IsNullOrWhiteSpace(x.RequiredField))
            .IsStatus(MyStatus.Complete).When(x => !string.IsNullOrWhiteSpace(x.RequiredField))
            .Build();
    }
}
```

When a new field is added an UndefinedStatusException will be thrown giving details of which type member has not been explicitly defined in a status definition.

Note that not all members need to be mapped in status definitions:

```
public class MyType
{
    private readonly IStatus<MyType, MyStatus> _myStatus;

    public MyStatus => _myStatus.Get(this);

    public string RequiredField { get; set; }

    public bool IrrelevantField { get; set; }

    public MyType()
    {
        _myStatus = StatusBuilder
            .BuildStatus<Status>()
            .FromType<MyType>(config => 
            {
                config.Ignore(x => x.IrrelevantField);
            })
            .IsStatus(MyStatus.Incomplete).When(x => string.IsNullOrWhiteSpace(x.RequiredField))
            .IsStatus(MyStatus.Complete).When(x => !string.IsNullOrWhiteSpace(x.RequiredField))
            .Build();
    }
}
```

and default statuses can be defined:

```
public class MyType
{
    private readonly IStatus<MyType, MyStatus> _myStatus;

    public MyStatus => _myStatus.Get(this);

    public string RequiredField { get; set; }

    public MyType()
    {
        _myStatus = StatusBuilder
            .BuildStatus<Status>()
            .FromType<MyType>()
            .IsStatus(MyStatus.Complete).When(x => !string.IsNullOrWhiteSpace(x.RequiredField))
            .IsStatus(MyStatus.Incomplete).ByDefault()
            .Build();
    }
}
```

and statuses cannot be defined more than once or have the same definition (otherwise an AmbiguousStatusException will be thrown):

```
public class MyType
{
    private readonly IStatus<MyType, MyStatus> _myStatus;

    public MyStatus => _myStatus.Get(this);

    public string RequiredField { get; set; }

    public MyType()
    {
        _myStatus = StatusBuilder
            .BuildStatus<Status>()
            .FromType<MyType>()
            .IsStatus(MyStatus.Complete).When(x => !string.IsNullOrWhiteSpace(x.RequiredField))
            .IsStatus(MyStatus.Incomplete).When(x => !string.IsNullOrWhiteSpace(x.RequiredField))
            .Build();
    }
}
```

