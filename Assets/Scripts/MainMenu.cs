using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public GameObject[] UI;

    [System.Serializable]
    public class _Petunjuk
    {
        public Sprite image;
        public string text;
    }

    public _Petunjuk[] petunjuk;
    public int index_petunjuk;

    [System.Serializable]
    public class _PetunjukObject
    {
        public Image image;
        public Text text;
        public Text text_2;
        public GameObject onlyText;
        public GameObject withImage;
    }

    public _PetunjukObject petunjukObject;

    [System.Serializable]
    public class _Materi
    {
        public Sprite image;
        public string text;
    }

	[HideInInspector]
    public List<_Materi> materi;
    public int index_materi;


    [System.Serializable]
    public class _MateriObject
    {
        public Image image;
        public Text text;
        public Text text_2;
        public GameObject onlyText;
        public GameObject withImage;
    }

    public _MateriObject materiObject;

    [System.Serializable]
    public class _Question
    {
        public Sprite image;
        public string text;
        public string answer;
    }

    public _Question[] question;

    [System.Serializable]
    public class _QuestionObject
    {
        public Image image;
        public Text skor;
        public Text nomor;
        public Text text;
        public Text text_2;
        public GameObject onlyText;
        public GameObject withImage;
    }

    public _QuestionObject questionObject;
    public int index_question;

    public Text[] answerText;
    public GameObject[] answerObject;
    public string answer;
    public int score;
    public int[] index;

    public Text feedback;
    public Text feedbackSkor;
    public Text total;

    public GameObject buttonInfo;

    [System.Serializable]
    public class _Info
    {
        public Sprite image;
        public string title;
        public string text;
    }

    public _Info[] Info;

    [System.Serializable]
    public class _InfoObject
    {
        public Image image;
        public Text text;
        public Text text_2;
        public Text title;
        public GameObject onlyText;
        public GameObject withImage;
    }

    public _InfoObject InfoObject;
	public GameObject Benar;
	public GameObject Salah;

    public InputField nama_input;
    public Text[] skor_tertinggi;
    public GameObject nama_object;
    public Text[] nama_tertinggi;
    public GameObject[] skor_tertinggi_object;
    public InputField mulai_nama_input;

    public Text[] nama;
    public Text[] skor;

    public List<int> _listSkor;
    public List<string> _listNama;

    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public class Score
    {
        public int score;
        public string nama;

        public Score(int _score, string _nama)
        {
            this.score = _score;
            this.nama = _nama;
        }
    }

    public List<Score> _listScore;

    // Use this for initialization
    private void Start()
    {
        materiObject.withImage.SetActive(false);
        materiObject.onlyText.SetActive(false);

        petunjukObject.withImage.SetActive(false);
        petunjukObject.onlyText.SetActive(false);

        if (petunjuk[index_petunjuk].image != null)
        {
            petunjukObject.image.sprite = petunjuk[index_petunjuk].image;
            petunjukObject.text.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");
            petunjukObject.text_2.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");

            petunjukObject.withImage.SetActive(true);
        }
        else
        {
            petunjukObject.text.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");
            petunjukObject.text_2.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");

            petunjukObject.onlyText.SetActive(true);
        }

		materi = new List<_Materi>();
		materi.Add (new _Materi(){ image = null, text="Loading..."});
		if (materi[0].image != null)
		{
			materiObject.image.sprite = materi[0].image;
			materiObject.text.text = materi[0].text;
			materiObject.text_2.text = materi[0].text;

			materiObject.withImage.SetActive(true);
		}
		else
		{
			materiObject.text.text = materi[0].text;
			materiObject.text_2.text = materi[0].text;

			materiObject.onlyText.SetActive(true);
		}
        GetMateri();
    }

    private void GetMateri()
    {
        var temp = StartCoroutine(ProcessRequest("https://server-ar-jantung.000webhostapp.com/api.php"));
    }

    private void Getsoal() { 
        var temp = StartCoroutine(ProcessRequestSoal("https://server-ar-jantung.000webhostapp.com/api.php?data=soal"));
    }

    private IEnumerator ProcessRequestSoal(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);

                //StreamReader reader = new StreamReader(request.downloadHandler.text);
                //string jsonResponse = reader.ReadToEnd();
                SoalData info = JsonUtility.FromJson<SoalData>(request.downloadHandler.text);

                List<_Question> temp = new List<_Question>();
                for (int i = 0; i < info.soal.Count; i++)
                {
                    temp.Add(new _Question() { image = null, text = info.soal[i].pertanyaan, answer = info.soal[i].jawaban });
                }
                question = temp.ToArray();
            }
            Disable();
            index_question = 0;
            score = 0;
            index = RandomArray.StartShuffle(question.Length);
            //Debug.Log(index[index_question]);
            ShowQuestion(index[index_question] - 1);
            UI[16].SetActive(true);
            mulai_nama_input.text = "";

            int skor = PlayerPrefs.GetInt("skor_tertinggi");
            string nama = PlayerPrefs.GetString("nama_tertinggi");
            if (skor == 0)
            {
                for (int i = 0; i < skor_tertinggi_object.Length; i++)
                {
                    skor_tertinggi_object[i].SetActive(false);
                }
                nama_object.SetActive(false);
            }
            else
            {
                for (int i = 0; i < skor_tertinggi_object.Length; i++)
                {
                    skor_tertinggi_object[i].SetActive(true);
                }
                nama_object.SetActive(true);
                for (int i = 0; i < skor_tertinggi.Length; i++)
                {
                    skor_tertinggi[i].text = "Skor Tertinggi : " + skor;
                }
                for (int i = 0; i < nama_tertinggi.Length; i++)
                {
                    nama_tertinggi[i].text = "Nama : " + nama;
                }
            }
        }
    }

    private IEnumerator ProcessRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);

                //StreamReader reader = new StreamReader(request.downloadHandler.text);
                //string jsonResponse = reader.ReadToEnd();
                MateriData info = JsonUtility.FromJson<MateriData>(request.downloadHandler.text);

                materi = new List<_Materi>();
                for (int i = 0; i < info.materi.Count; i++)
                {
					if(info.materi[i].gambar == ""){
						materi.Add(new _Materi(){ image = null, text=info.materi[i].materi});
						var temp = materi[i].image;
						if (materi[i].image != null)
						{
							materiObject.image.sprite = materi[i].image;
							materiObject.text.text = materi[i].text;
							materiObject.text_2.text = materi[i].text;

							materiObject.withImage.SetActive(true);
							materiObject.onlyText.SetActive(false);
						}
						else
						{
							materiObject.text.text = materi[i].text;
							materiObject.text_2.text = materi[i].text;

							materiObject.withImage.SetActive(false);
							materiObject.onlyText.SetActive(true);
						}
					}else{
						yield return StartCoroutine(GetTexture(info.materi[i].gambar,info.materi[i].materi));
					}
                }
            }
        }
    }
	
	private IEnumerator GetTexture(String uri,String text) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://server-ar-jantung.000webhostapp.com/"+uri);
        yield return www.SendWebRequest();

        Texture myTexture = DownloadHandlerTexture.GetContent(www);
		Texture2D text2D = (Texture2D)myTexture;
		Sprite mySprite = Sprite.Create(text2D, new Rect(0.0f, 0.0f, text2D.width, text2D.height), new Vector2(0.5f, 0.5f), 100.0f);
		materi.Add(new _Materi(){ image = mySprite, text=text});
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Disable()
    {
        for (int i = 0; i < UI.Length; i++)
        {
            UI[i].SetActive(false);
        }
    }

    public void MulaiAR()
    {
        SceneManager.LoadScene("AR");
    }

    public void Materi()
    {
        Disable();
        UI[1].SetActive(true);
    }

    public void Reset()
    {
        for (int i = 10; i > 0; i--)
        {
            //PlayerPrefs.SetInt("skor_tertinggi_" + i, 0);
            //PlayerPrefs.SetString("nama_tertinggi_" + i, "-");
            Debug.Log(i);
        }
        LeaderBoard();
    }

    public void LatihanSoal()
    {
        Getsoal();
        
    }

    public void Mulai()
    {
        Disable();
        UI[5].SetActive(true);
    }
    public void MulaiNama()
    {
        if (mulai_nama_input.text.Length > 0)
        {
            if (question.Length > 0)
            {
                Disable();
                UI[5].SetActive(true);
                PlayerPrefs.SetString("nama_sementara", mulai_nama_input.text);
            }
        }
        else
        {
            Disable();
            UI[16].SetActive(true);
            UI[20].SetActive(true);
            StartCoroutine(HideNama());
        }
    }


    IEnumerator HideNama()
    {
        yield return new WaitForSecondsRealtime(1);
        Disable();
        UI[16].SetActive(true);
    }

    public void ShowQuestion(int index)
    {
        for (int i = 0; i < answerText.Length; i++)
        {
            answerText[i].text = "";
            answerObject[i].SetActive(false);
        }
        questionObject.withImage.SetActive(false);
        questionObject.onlyText.SetActive(false);
        if (question[index].image != null)
        {
            questionObject.image.sprite = question[index].image;
            questionObject.text.text = question[index].text;
            questionObject.text_2.text = question[index].text;

            questionObject.withImage.SetActive(true);
        }
        else
        {
            questionObject.text.text = question[index].text;
            questionObject.text_2.text = question[index].text;

            questionObject.onlyText.SetActive(true);
        }
        questionObject.skor.text = "Skor : " + score.ToString();
        questionObject.nomor.text = "" + (index_question + 1).ToString();
    }

    public void Petunjuk()
    {
        Disable();
        UI[2].SetActive(true);
    }

    public void Informasi()
    {
        Disable();
        UI[3].SetActive(true);
    }

    public void Keluar()
    {
        Disable();
        UI[4].SetActive(true);
    }

    public void Beranda()
    {
        Disable();
        UI[0].SetActive(true);
        mulai_nama_input.text = "";
    }

    public void NextMateri()
    {
        materiObject.withImage.SetActive(false);
        materiObject.onlyText.SetActive(false);

        if (index_materi + 1 == materi.Count)
        {
            index_materi = 0;
        }
        else
        {
            index_materi = index_materi + 1;
        }
        var temp = materi[index_materi].image;
        Debug.Log(temp);
        if (materi[index_materi].image != null)
        {
            materiObject.image.sprite = materi[index_materi].image;
            materiObject.text.text = materi[index_materi].text;
            materiObject.text_2.text = materi[index_materi].text;

            materiObject.withImage.SetActive(true);
        }
        else
        {
            materiObject.text.text = materi[index_materi].text;
            materiObject.text_2.text = materi[index_materi].text;

            materiObject.onlyText.SetActive(true);
        }
    }

    public void PrevMateri()
    {
        materiObject.withImage.SetActive(false);
        materiObject.onlyText.SetActive(false);
        if (index_materi - 1 < 0)
        {
            index_materi = materi.Count - 1;
        }
        else
        {
            index_materi = index_materi - 1;
        }

        if (materi[index_materi].image != null)
        {
            materiObject.image.sprite = materi[index_materi].image;
            materiObject.text.text = materi[index_materi].text;
            materiObject.text_2.text = materi[index_materi].text;

            materiObject.withImage.SetActive(true);
        }
        else
        {
            materiObject.text.text = materi[index_materi].text;
            materiObject.text_2.text = materi[index_materi].text;

            materiObject.onlyText.SetActive(true);
        }
    }

    public void NextPetunjuk()
    {
        petunjukObject.withImage.SetActive(false);
        petunjukObject.onlyText.SetActive(false);

        if (index_petunjuk + 1 == petunjuk.Length)
        {
            index_petunjuk = 0;
        }
        else
        {
            index_petunjuk = index_petunjuk + 1;
        }
        if (petunjuk[index_petunjuk].image != null)
        {
            petunjukObject.image.sprite = petunjuk[index_petunjuk].image;
            petunjukObject.text.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");
            petunjukObject.text_2.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");

            petunjukObject.withImage.SetActive(true);
        }
        else
        {
            petunjukObject.text.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");
            petunjukObject.text_2.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");

            petunjukObject.onlyText.SetActive(true);
        }
    }

    public void PrevPetunjuk()
    {
        petunjukObject.withImage.SetActive(false);
        petunjukObject.onlyText.SetActive(false);
        if (index_petunjuk - 1 < 0)
        {
            index_petunjuk = petunjuk.Length - 1;
        }
        else
        {
            index_petunjuk = index_petunjuk - 1;
        }

        if (petunjuk[index_petunjuk].image != null)
        {
            petunjukObject.image.sprite = petunjuk[index_petunjuk].image;
            petunjukObject.text.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");
            petunjukObject.text_2.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");

            petunjukObject.withImage.SetActive(true);
        }
        else
        {
            petunjukObject.text.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");
            petunjukObject.text_2.text = petunjuk[index_petunjuk].text.Replace("<br>", "\n");

            petunjukObject.onlyText.SetActive(true);
        }
    }

    public void Tombol(string Huruf)
    {
        if (answer.Length < 12)
        {
            for (int i = 0; i < answerText.Length; i++)
            {
                answerText[i].text = "";
				answerObject[i].SetActive(false);
            }
            answer = answer.ToUpper() + Huruf.ToUpper();
            for (int i = 0; i < answer.Length; i++)
            {
                answerText[i].text = answer[i].ToString().ToUpper();
				answerObject[i].SetActive(true);
            }
        }
    }

    public void TombolDelete()
    {
        if (answer.Length > 0)
        {
            for (int i = 0; i < answerText.Length; i++)
            {
                answerText[i].text = "";
				answerObject[i].SetActive(false);
            }
            answer = answer.Remove(answer.Length - 1, 1); ;
            for (int i = 0; i < answer.Length; i++)
            {
                answerText[i].text = answer[i].ToString();
				answerObject[i].SetActive(true);
            }
        }
    }

    public void TombolSpace()
    {
        if (answer.Length < 12)
        {
            for (int i = 0; i < answerText.Length; i++)
            {
                answerText[i].text = "";
				answerObject[i].SetActive(false);
            }
            answer = answer + " ";
            for (int i = 0; i < answer.Length; i++)
            {
                answerText[i].text = answer[i].ToString();
				answerObject[i].SetActive(true);
            }
        }
    }

    public void TombolEnter()
    {
        Disable();
        UI[6].SetActive(true);

        int[] search = BoyerMoore.SearchString(answer.ToUpper(), question[index[index_question] - 1].answer.ToUpper());

        if (search.Length > 0)
        {
            score = score + 10;
            feedback.text = "JAWABAN BENAR";
            feedbackSkor.text = "Skor : " + score.ToString();
			Benar.SetActive(true);
			Salah.SetActive(false);
        }
        else
        {
            feedback.text = "JAWABAN SALAH";
            feedbackSkor.text = "Skor : " + score.ToString();
			Benar.SetActive(false);
			Salah.SetActive(true);
        }
        for (int i = 0; i < answerText.Length; i++)
        {
            answerText[i].text = "";
        }
        answer = "";
        StartCoroutine(HideFeedback());
    }

    private IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(2);
        Disable();

        index_question = index_question + 1;

        if (index_question < question.Length)
        {
            ShowQuestion(index[index_question] - 1);
            UI[5].SetActive(true);
        }
        else
        {
            total.text = "SKOR : "+score.ToString();
            UI[17].SetActive(true);
            int skor = PlayerPrefs.GetInt("skor_tertinggi");
            string nama = PlayerPrefs.GetString("nama_tertinggi");

            int skor1 = PlayerPrefs.GetInt("skor_tertinggi_1");
            int skor2 = PlayerPrefs.GetInt("skor_tertinggi_2");
            int skor3 = PlayerPrefs.GetInt("skor_tertinggi_3");
            int skor4 = PlayerPrefs.GetInt("skor_tertinggi_4");
            int skor5 = PlayerPrefs.GetInt("skor_tertinggi_5");
            int skor6 = PlayerPrefs.GetInt("skor_tertinggi_6");
            int skor7 = PlayerPrefs.GetInt("skor_tertinggi_7");
            int skor8 = PlayerPrefs.GetInt("skor_tertinggi_8");
            int skor9 = PlayerPrefs.GetInt("skor_tertinggi_9");
            int skor10 = PlayerPrefs.GetInt("skor_tertinggi_10");

            List<int> listSkor = new List<int>();

            listSkor.Add(skor1);
            listSkor.Add(skor2);
            listSkor.Add(skor3);
            listSkor.Add(skor4);
            listSkor.Add(skor5);

            listSkor.Add(skor6);
            listSkor.Add(skor7);
            listSkor.Add(skor8);
            listSkor.Add(skor9);
            listSkor.Add(skor10);

            string nama1 = PlayerPrefs.GetString("nama_tertinggi_1");
            string nama2 = PlayerPrefs.GetString("nama_tertinggi_2");
            string nama3 = PlayerPrefs.GetString("nama_tertinggi_3");
            string nama4 = PlayerPrefs.GetString("nama_tertinggi_4");
            string nama5 = PlayerPrefs.GetString("nama_tertinggi_5");
            string nama6 = PlayerPrefs.GetString("nama_tertinggi_6");
            string nama7 = PlayerPrefs.GetString("nama_tertinggi_7");
            string nama8 = PlayerPrefs.GetString("nama_tertinggi_8");
            string nama9 = PlayerPrefs.GetString("nama_tertinggi_9");
            string nama10 = PlayerPrefs.GetString("nama_tertinggi_10");

            List<string> listNama = new List<string>();

            listNama.Add(nama1);
            listNama.Add(nama2);
            listNama.Add(nama3);
            listNama.Add(nama4);
            listNama.Add(nama5);

            listNama.Add(nama6);
            listNama.Add(nama7);
            listNama.Add(nama8);
            listNama.Add(nama9);
            listNama.Add(nama10);

            int skor_terendah = 0;
            int index = 0;

            _listSkor = listSkor;
            _listNama = listNama;

            _listScore = new List<Score>();

            for (int i = 0; i < listSkor.Count; i++)
            {
                _listScore.Add(new Score(listSkor[i], listNama[i]));
            }

            _listScore.Add(new Score(score, mulai_nama_input.text));

            List<Score> sort = _listScore.OrderByDescending(x => x.score).ToList();

            if (score > 0)
            {
                SetLeaderBoard(sort);
            }

            if (int.Parse(score.ToString()) > skor)
            {
                nama_object.SetActive(true);
            }
            else
            {
                nama_object.SetActive(false);
            }
            if (skor > 0)
            {
                for (int i = 0; i < skor_tertinggi_object.Length; i++)
                {
                    skor_tertinggi_object[i].SetActive(true);
                }
                nama_object.SetActive(true);
                for (int i = 0; i < skor_tertinggi.Length; i++)
                {
                    skor_tertinggi[i].text = "Skor Tertinggi : " + skor;
                }
                for (int i = 0; i < nama_tertinggi.Length; i++)
                {
                    nama_tertinggi[i].text = "Nama : " + nama;
                }
            }
        }
    }

    public void SetLeaderBoard(List<Score> listSkor)
    {
        for (int i = 0; i > listSkor.Count; i--)
        {
            Debug.Log(i + "|" + listSkor[i].score + "|" + listSkor[i].nama);
        }

        for (int i = 10; i > 0; i--)
        {
            Debug.Log(i+"|"+listSkor[i - 1].score+"|"+ listSkor[i - 1].nama);
            PlayerPrefs.SetInt("skor_tertinggi_" + i, listSkor[i - 1].score);
            PlayerPrefs.SetString("nama_tertinggi_" + i, listSkor[i - 1].nama);
        }
    }

    public void LeaderBoard()
    {

        int skor1 = PlayerPrefs.GetInt("skor_tertinggi_1");
        int skor2 = PlayerPrefs.GetInt("skor_tertinggi_2");
        int skor3 = PlayerPrefs.GetInt("skor_tertinggi_3");
        int skor4 = PlayerPrefs.GetInt("skor_tertinggi_4");
        int skor5 = PlayerPrefs.GetInt("skor_tertinggi_5");
        int skor6 = PlayerPrefs.GetInt("skor_tertinggi_6");
        int skor7 = PlayerPrefs.GetInt("skor_tertinggi_7");
        int skor8 = PlayerPrefs.GetInt("skor_tertinggi_8");
        int skor9 = PlayerPrefs.GetInt("skor_tertinggi_9");
        int skor10 = PlayerPrefs.GetInt("skor_tertinggi_10");

        List<int> listSkor = new List<int>();

        listSkor.Add(skor1);
        listSkor.Add(skor2);
        listSkor.Add(skor3);
        listSkor.Add(skor4);
        listSkor.Add(skor5);

        listSkor.Add(skor6);
        listSkor.Add(skor7);
        listSkor.Add(skor8);
        listSkor.Add(skor9);
        listSkor.Add(skor10);

        string nama1 = PlayerPrefs.GetString("nama_tertinggi_1");
        string nama2 = PlayerPrefs.GetString("nama_tertinggi_2");
        string nama3 = PlayerPrefs.GetString("nama_tertinggi_3");
        string nama4 = PlayerPrefs.GetString("nama_tertinggi_4");
        string nama5 = PlayerPrefs.GetString("nama_tertinggi_5");
        string nama6 = PlayerPrefs.GetString("nama_tertinggi_6");
        string nama7 = PlayerPrefs.GetString("nama_tertinggi_7");
        string nama8 = PlayerPrefs.GetString("nama_tertinggi_8");
        string nama9 = PlayerPrefs.GetString("nama_tertinggi_9");
        string nama10 = PlayerPrefs.GetString("nama_tertinggi_10");

        List<string> listNama = new List<string>();

        listNama.Add(nama1);
        listNama.Add(nama2);
        listNama.Add(nama3);
        listNama.Add(nama4);
        listNama.Add(nama5);

        listNama.Add(nama6);
        listNama.Add(nama7);
        listNama.Add(nama8);
        listNama.Add(nama9);
        listNama.Add(nama10);

        _listSkor = listSkor;
        _listNama = listNama;

        Disable();

        UI[18].SetActive(true);

        for (int i = 1; i <= 10; i++)
        {
            string tempNama = PlayerPrefs.GetString("nama_tertinggi_" + i, "-");
            nama[i - 1].text = tempNama;
            skor[i - 1].text = PlayerPrefs.GetInt("skor_tertinggi_" + i, 0).ToString();
        }
    }

    public void Simpan()
    {
        string nama = nama_input.text;
        int skor = int.Parse(score.ToString());
        PlayerPrefs.SetString("nama_tertinggi", nama);
        PlayerPrefs.SetInt("skor_tertinggi", skor);
        Disable();

        UI[0].SetActive(true);
    }

    public void InformasiObjek()
    {
        Disable();
        print("-1");
        UI[9].SetActive(true);
        InfoObject.withImage.SetActive(false);
        InfoObject.onlyText.SetActive(false);
        print(UI[9].activeSelf);
        switch (GameManager.instance.IndexDetectMarker)
        {
            case 0:
                if (Info[0].image != null)
                {
                    InfoObject.image.sprite = Info[0].image;
                    InfoObject.text.text = Info[0].text.Replace("<br>", "\n");
                    InfoObject.text_2.text = Info[0].text.Replace("<br>", "\n");
                    InfoObject.title.text = Info[0].title.Replace("<br>", "\n");

                    InfoObject.withImage.SetActive(true);
                }
                else
                {
                    InfoObject.text.text = Info[0].text.Replace("<br>", "\n");
                    InfoObject.text_2.text = Info[0].text.Replace("<br>", "\n");
                    InfoObject.title.text = Info[0].title.Replace("<br>", "\n");

                    InfoObject.onlyText.SetActive(true);
                }
                break;

            case 1:
                switch (GameManager.instance.IndexDetailObject)
                {
                    case -1:
                        if (Info[1].image != null)
                        {
                            InfoObject.image.sprite = Info[1].image;
                            InfoObject.text.text = Info[1].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[1].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[1].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[1].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[1].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[1].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;

                    case 0:
                        if (Info[4].image != null)
                        {
                            InfoObject.image.sprite = Info[4].image;
                            InfoObject.text.text = Info[4].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[4].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[4].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[4].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[4].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[4].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;
                }
                break;

            case 2:

                switch (GameManager.instance.IndexDetailObject)
                {
                    case -1:
                        if (Info[2].image != null)
                        {
                            InfoObject.image.sprite = Info[2].image;
                            InfoObject.text.text = Info[2].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[2].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[2].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[2].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[2].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[2].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;

                    case 0:
                        if (Info[5].image != null)
                        {
                            InfoObject.image.sprite = Info[5].image;
                            InfoObject.text.text = Info[5].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[5].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[5].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[5].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[5].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[5].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;

                    case 1:
                        if (Info[6].image != null)
                        {
                            InfoObject.image.sprite = Info[6].image;
                            InfoObject.text.text = Info[6].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[6].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[6].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[6].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[6].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[6].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;
                }
                break;

            case 3:
                switch (GameManager.instance.IndexDetailObject)
                {
                    case -1:
                        if (Info[3].image != null)
                        {
                            InfoObject.image.sprite = Info[3].image;
                            InfoObject.text.text = Info[3].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[3].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[3].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[3].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[3].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[3].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;

                    case 0:
                        if (Info[7].image != null)
                        {
                            InfoObject.image.sprite = Info[7].image;
                            InfoObject.text.text = Info[7].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[7].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[7].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[7].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[7].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[7].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;

                    case 1:
                        if (Info[8].image != null)
                        {
                            InfoObject.image.sprite = Info[8].image;
                            InfoObject.text.text = Info[8].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[8].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[8].title.Replace("<br>", "\n");

                            InfoObject.withImage.SetActive(true);
                        }
                        else
                        {
                            InfoObject.text.text = Info[8].text.Replace("<br>", "\n");
                            InfoObject.text_2.text = Info[8].text.Replace("<br>", "\n");
                            InfoObject.title.text = Info[8].title.Replace("<br>", "\n");

                            InfoObject.onlyText.SetActive(true);
                        }
                        break;
                }
                break;

            default:
                print("Error");
                break;
        }
        UI[9].SetActive(true);
    }

    public void DetailObjek(int index)
    {
        GameManager.instance.IndexDetailObject = index;
        Disable();
        UI[8].SetActive(true);
        UI[13].SetActive(true);
        switch (GameManager.instance.IndexDetectMarker)
        {
            case 0:
                break;

            case 1:
                switch (index)
                {
                    case 0:
                        GameManager.instance.controller.Disable();
                        GameManager.instance.controller.Object[4].SetActive(true);
                        break;
                }
                break;

            case 2:
                switch (index)
                {
                    case 0:
                        GameManager.instance.controller.Disable();
                        GameManager.instance.controller.Object[5].SetActive(true);
                        break;

                    case 1:
                        GameManager.instance.controller.Disable();
                        GameManager.instance.controller.Object[6].SetActive(true);
                        break;
                }
                break;

            case 3:
                switch (index)
                {
                    case 0:
                        GameManager.instance.controller.Disable();
                        GameManager.instance.controller.Object[7].SetActive(true);
                        break;

                    case 1:
                        GameManager.instance.controller.Disable();
                        GameManager.instance.controller.Object[8].SetActive(true);
                        break;
                }
                break;
        }
    }

    public void KembaliObjek()
    {
        Disable();
        UI[8].SetActive(true);
        UI[13].SetActive(false);
        switch (GameManager.instance.IndexDetectMarker)
        {
            case 0:
                break;

            case 1:
                GameManager.instance.controller.Disable();
                GameManager.instance.controller.Object[1].SetActive(true);
                UI[10].SetActive(true);
                break;

            case 2:
                GameManager.instance.controller.Disable();
                GameManager.instance.controller.Object[2].SetActive(true);
                UI[11].SetActive(true);
                break;

            case 3:
                GameManager.instance.controller.Disable();
                GameManager.instance.controller.Object[3].SetActive(true);
                UI[12].SetActive(true);
                break;
        }
        GameManager.instance.IndexDetailObject = -1;
    }

    public void Main()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Kembali()
    {
        Disable();
        UI[8].SetActive(true);
    }
    public void KembaliVideo()
    {
        Disable();
        UI[9].SetActive(true);
        audioSource.Stop();
    }

    public void Tutup()
    {
        Disable();
        UI[17].SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenVideo()
    {
    }

    public void Video()
    {
        Disable();
        UI[19].SetActive(true);
        int index = GameManager.instance.IndexDetectMarker;

        audioSource.Stop();
        videoPlayer.clip = videoClips[index];
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}