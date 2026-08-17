using Microsoft.AspNetCore.Http.HttpResults;
using SQLitePCL;

public class FeatureRepository : IFeatureRepository
{
    private readonly AppDbContext _context;

    public FeatureRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Feature> GetAll()
    {
        return _context.Features.ToList();
    }

    public Feature? GetById(int id)
    {
        return _context.Features.Find(id);
    }

    public Feature? Update(int id, bool isEnabled)
    {
        var feature = _context.Features.Find(id);
        if (feature == null)
            return null;

        //isEnabled dışarıdan (Service'ten, o da Controller'dan, o da Angular'dan HTTP isteğiyle) alacak.
        feature.IsEnabled = isEnabled;
        _context.SaveChanges();
        return feature;
    }

    public void Add(Feature feature)
    {
        _context.Features.Add(feature);
        _context.SaveChanges();
    }
}