# Stock-Village
![image](https://user-images.githubusercontent.com/78673570/181470730-a0291a27-c51d-4b51-a175-f1821f6ed6d7.png)

<br>

## 📈 프로젝트 소개
코로나19 이후 ‘초보’ 개인투자자들이 증가한 것에서 아이디어를 얻어, ‘게이미피케이션 요소를 활용한 주식 데이터 분석 및 시각화 서비스’를 제공하여 사전 지식이 부족하더라도 누구나 쉽게 주식 시장 정보를 파악할 수 있는 플랫폼을 제시하고자 하였습니다.

(* 게이미피케이션 : 게임이 아닌 분야에 대한 지식 전달, 행동 및 관심 유도 혹은 마케팅 등에 게임의 매커니즘, 사고방식과 같은 게임의 요소를 접목시키는 것)

<br>

핵심 기능은 다음과 같습니다.
1. 주식 데이터 Unity3D 화면 상에 시각화
2. 분석 내용을 바탕으로 새롭게 제안하는 종목별 관심도 정보 제공

<br><br>

##  🛠 기술 스택
- 시각화를 위한 Client 환경으로 `Untiy3D`를 사용하였습니다.
- `Pandas` 라이브러리를 사용해 데이터를 분석을 진행하였습니다.
- 분석 결과를 Client로 전송하기 위해 `AWS EC2`를 사용하였습니다.
- 오픈 API(`Yahoo Finance API`, `Fear & Greed Index API`, `Finbert`)에서 데이터를 제공받았습니다.
- 협업 및 코드 버전 관리를 위해 Unity에서 제공하는 `Unity Collaborate` 를 사용하였습니다.

<br><br>

## 👩‍💻 개발 내용
- 데이터 시각화 - 주식 종목을 하나의 건물로 취급하여 ‘도시’ 컨셉으로 화면 구현
- 주가, 거래량 등 기본적인 주식 정보를 Unity3D 상에서 제공
- Youtube Data API를 통해 종목별 유튜브 데이터 수집 및 전처리 (영상 제목, 조회수 등)
- Pandas 라이브러리를 활용한 주식 - 유튜브 데이터 간 상관관계분석
- 분석 내용을 바탕으로 새롭게 제안하는 종목별 관심도 정보 제공

<br><br>

## 📑 문서
- [학술저널] [게이미피케이션을 활용한 주식 포트폴리오 분석 및 시각화 서비스](https://www.dbpia.co.kr/journal/articleDetail?nodeId=NODE11026767)

<br><br>

## 🧩 서비스 아키텍처
![image](https://user-images.githubusercontent.com/78673570/181473339-7497e66b-14b5-4bc0-a47b-8fdf7933e47d.png)

<br><br>

## 🔍 화면 구성
![image](https://user-images.githubusercontent.com/78673570/181473547-7f5f271b-6556-4eed-8e64-c346167e45ca.png)
![image](https://user-images.githubusercontent.com/78673570/181473670-dc12ad9d-57db-4f64-bdb4-5e17d83dcd3c.png)
