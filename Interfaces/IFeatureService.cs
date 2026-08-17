public interface IFeatureService
{
    IEnumerable<Feature> GetAllFeatures();
    Feature? UpdateFeature(int id, bool isEnabled);
    Feature CreateFeature(Feature feature);
    Feature? GetFeatureById(int id);

}