namespace Infrastructure;

public interface ISeedData
{
    Task EnsurePopulated(bool dropExisting = false);
}