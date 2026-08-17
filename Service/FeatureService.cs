public class FeatureService : IFeatureService
{
    private readonly IFeatureRepository _repository;
    public FeatureService (IFeatureRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Feature> GetAllFeatures()
    {
        return _repository.GetAll();
    }
    public Feature? UpdateFeature(int id, bool isEnabled)
    {
        return _repository.Update(id,isEnabled);
    }
    public Feature CreateFeature(Feature feature)
    {
        _repository.Add(feature);
        return feature;
    }
    
    public Feature? GetFeatureById(int id)
    {
        return _repository.GetById(id);
    }



}