![image](https://github.com/user-attachments/assets/a23fcf4c-0be7-43c9-8c7c-ba3d3ac58525)

# 0. Bulid (빌드 배포)
[빌드 링크]

<br/>
<br/>
# 1. Project Overview (프로젝트 개요)
- 프로젝트 이름: NightmareDash
- 프로젝트 설명: 유니티 입문 개발 팀 프로젝트
- 게임 설명 : 공포 2D 런게임
- 플로우 차트
플로우 차트 사진 and link
<br/>

# 2. Team Members (팀원 및 팀 소개)
| 김민준 | 이지영 | 양원준 | 성현창 | 김아연 |
|:------:|:------:|:------:|:------:|:------:|
| Programmer/PL | Programmer | Programmer | Programmer | Programmer |
| [GitHub](https://github.com/Toaaaa) | [GitHub](https://github.com/LeeJiyoung0511) | [GitHub](https://github.com/kierwl) | [GitHub](https://github.com/SHC7065) | [GitHub](https://github.com/borigulmi) |

<br/>

# 3. Key Features (주요 기능)
<br/>
PPT링크
<br/>

# 4. Tasks & Responsibilities (작업 및 역할 분담)
|  |  |  |
|-----------------|-----------------|-----------------|
| 김민준   | <ul><li>플레이어</li><li>맵</li><li>장애물</li><li>아이템</li></ul>     |
| 이지영   | <ul><li>스토리 소개 연출</li><li>튜로리얼 개발</li><li>인게임UI</li><li>재화</li><li>점프스케어</li></ul>      |
| 양원준   | <ul><li>배경</li><li>메인로비</li><li>플레이어 커스터마이징</li><li>업적</li><li>공포 연출</li></ul>            |
| 성현창   | <ul><li>플레이어</li><li>사운드</li><li>시작 화면</li></ul>     |
| 김아연   | <ul><li>UI디자인</li><li>뽑기</li><li>펫</li><li>유물</li></ul>     |

<br/>

# 5. Technology Stack (기술 스택)
## 5.1 Language
|  |  |
|-----------------|-----------------|
| C#|<img src="https://github.com/user-attachments/assets/2e122e74-a28b-4ce7-aff6-382959216d31" alt="C#" width="100">| 


<br/>

## 5.4 Cooperation
|  |  |
|-----------------|-----------------|
| Git    |  <img src="https://github.com/user-attachments/assets/483abc38-ed4d-487c-b43a-3963b33430e6" alt="git" width="100">    |
| Notion    |  <img src="https://github.com/user-attachments/assets/34141eb9-deca-416a-a83f-ff9543cc2f9a" alt="Notion" width="100">    |

<br/>

# 6. Development Workflow (개발 워크플로우)
## 브랜치 전략 (Branch Strategy)
우리의 브랜치 전략은 Git Flow를 기반으로 하며, 다음과 같은 브랜치를 사용합니다.

- Main Branch
  - 배포 가능한 상태의 코드를 유지합니다.
  - 모든 배포는 이 브랜치에서 이루어집니다.

- Dev Branch
  - 개발을 한뒤 이 브런치로 머지합니다.
  
- {name}(이름 대문자 스펠링) BranchName(기능 이름)
  - 팀원 각자의 개발 브랜치입니다.
  - 모든 기능 개발은 이 브랜치에서 이루어집니다.

<br/>

# 7. Coding Convention

## 명명 규칙

* 상수 : 영문 대문자 + 스네이크 케이스

* private 변수 : carmel

* public 변수 & 함수 : Pascal

<br/>

## 블록 구문
```
// 한 줄짜리 블록일 경우라도 {}를 생략하지 않고, 명확히 줄 바꿈 하여 사용한다
// good
if(true){
  return 'hello'
}

// bad
if(true) return 'hello'
```
<br/>

# 8. 커밋 컨벤션
## 기본 구조
```
type : subject

body 
```
<br/>

## type 종류
```
feat : 기능추가
comment : 주석 추가,수정
style : {}위치 공백추가,삭제,단순 변수명 변경
refactor : 코드 최적화
fix : 버그수정
```
<br/>
