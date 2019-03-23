using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour {

    public RankUser ranking;
    public TextMeshProUGUI rankPositionText;
    public TextMeshProUGUI usernamePositionText;
    public TextMeshProUGUI scorePositionText;
    public TextMeshProUGUI userSchoolNameText;
    public Image Image_RankPosition;
    public Image Image_RankName;
    public Image Image_RankScore;
    public Image thisImage;
    public Color defaultColor_RankPosition;
    public Color defaultColor_rank;
    public DBOESCOLA dboUserSchool;

    public void UpdateUserRank() {
        rankPositionText.text = string.IsNullOrEmpty(ranking.posRank.ToString()) ? "-" : ranking.posRank.ToString() + "º";
        usernamePositionText.text = ranking.nameUser;
        scorePositionText.text = ranking.scoreRank.ToString();
        if (dboUserSchool != null) {
            userSchoolNameText.text = dboUserSchool.nomeEscola;
        }

    }

    public void UpdateEmptyRank() {
        rankPositionText.text = "-";
        usernamePositionText.text = "-";
        scorePositionText.text = "-";
        userSchoolNameText.text = "-";
    }

    public void UpdateColors(Color RankColor, Color PosRankColor) {
        Image_RankPosition.color = PosRankColor;
        thisImage.color = RankColor;
    }

    public void DefaulColor() {
        Image_RankPosition.color = defaultColor_RankPosition;
        thisImage.color = defaultColor_rank;
    }
}
