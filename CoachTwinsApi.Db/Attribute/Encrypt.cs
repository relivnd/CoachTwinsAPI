namespace CoachTwinsApi.Db.Attribute
{
    public class Encrypt: System.Attribute
    {
        
    }
    public class DontEncrypt: System.Attribute
    {
        /*
         * Don't use DB encryption for development purposes. Replace [Enrypt] with [DontEncrypt]
         * */
    }
}