using UnityEngine;

public class CharacterStructure
{
    public string CharacterId;
    public string CharacterName;
    public string PortraitPath;

    public Sprite GetSpriteFromFilePath(string path)
    {
        Sprite resultSprite = Resources.Load<Sprite>(path);

        if (resultSprite == null)
        {
            Debug.Log(path + "에 이미지 리소스가 존재하지 않습니다.");
            return null;
        }

        return resultSprite;
    }
}