using System.Collections.Generic;
using System.IO;

namespace SpriteHelper.NesSound
{
    public static class SoundDataReader
    {
        public static Dictionary<string, byte> GetSongs()
        {
            var lines = File.ReadAllLines(FileConstants.Sound);
            var result = new Dictionary<string, byte>();
            foreach (var line in lines)
            {
                if (!line.StartsWith("song_index_"))
                {
                    break;
                }

                var split = line.Split(new char[] { '=' }, 2);
                var name = split[0].Trim();
                var value = byte.Parse(split[1].Trim());
                result.Add(name, value);
            }

            return result;
        }
    }
}
