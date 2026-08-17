public interface IFeatureRepository
{
    IEnumerable<Feature> GetAll();
    Feature? GetById(int id);
    Feature? Update(int id, bool isEnabled);
    void Add(Feature feature);

}