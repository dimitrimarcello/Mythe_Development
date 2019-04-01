
public class Volumes {

    public Volumes()
    {
        master = -81f;
        game = -81f;
        music = -81f;
    }

    private float master;
    private float game;
    private float music;

    public void SetMasterVolume(float volume)
    {
        master = volume;
    }

    public void SetGameVolume(float volume)
    {
        game = volume;
    }

    public void SetMusicVolume(float volume)
    {
        music = volume;
    }

    public float GetMasterVolume()
    {
        return master;
    }

    public float GetGameVolume()
    {
        return game;
    }

    public float GetMusicVolume()
    {
        return music;
    }

}
