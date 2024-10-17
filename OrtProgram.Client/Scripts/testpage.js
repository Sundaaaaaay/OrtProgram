const testId = new URLSearchParams(window.location.search).get("testId");
const apiUrl = `https://localhost:44340/ort/tests/gettest${testId}`;
const submitUrl = `https://localhost:44340/ort/tests/submitanswers`;

let questions = [];
let currentPage = 0;
const questionsPerPage = 10;

// Хранение ответов пользователя
let userAnswers = [];

// Функция для получения теста с сервера
function fetchTest() {
  fetch(apiUrl)
    .then((response) => response.json())
    .then((data) => {
      questions = data.questions;
      displayQuestions();
    })
    .catch((error) => {
      console.error("Error fetching test:", error);
    });
}

// Функция для отображения вопросов (10 на одной странице)
function displayQuestions() {
  const startIndex = currentPage * questionsPerPage;
  const endIndex = Math.min(startIndex + questionsPerPage, questions.length);

  const questionContainer = document.getElementById("question-container");
  questionContainer.innerHTML = "";

  for (let i = startIndex; i < endIndex; i++) {
    const question = questions[i];

    // Создаем элементы для вопроса
    const questionElement = document.createElement("div");
    questionElement.classList.add("mb-4");

    const questionText = document.createElement("h4");
    questionText.textContent = `Question ${i + 1}: ${question.questionText}`;
    questionElement.appendChild(questionText);

    // Перемешиваем ответы
    shuffledAnswers = shuffleAnswers(question);

    // Варианты ответов
    shuffledAnswers.forEach((answer, index) => {
      const answerId = `answer-${i}-${index}`;
      const answerDiv = document.createElement("div");
      answerDiv.classList.add("form-check");

      const answerInput = document.createElement("input");
      answerInput.type = "radio";
      answerInput.name = `answer-${i}`;
      answerInput.id = answerId;
      answerInput.classList.add("form-check-input");
      answerInput.value = answer.text; // Сохраняем значение ответа

      const answerLabel = document.createElement("label");
      answerLabel.htmlFor = answerId;
      answerLabel.classList.add("form-check-label");
      answerLabel.textContent = `${String.fromCharCode(65 + index)}) ${
        answer.text
      }`;

      answerDiv.appendChild(answerInput);
      answerDiv.appendChild(answerLabel);
      questionElement.appendChild(answerDiv);
    });

    questionContainer.appendChild(questionElement);
  }

  togglePaginationButtons(endIndex);
}

// Функция для перемешивания ответов
function shuffleAnswers(question) {
  const answers = [
    { text: question.firstAnswer },
    { text: question.secondAnswer },
    { text: question.thirdAnswer },
    { text: question.rightAnswer },
  ];

  return answers.sort(() => Math.random() - 0.5); // Перемешиваем
}

// Функция для переключения видимости кнопок "Previous" и "Next"
function togglePaginationButtons(endIndex) {
  document.getElementById("prev-question-btn").disabled = currentPage === 0;
  document.getElementById("next-question-btn").disabled =
    endIndex >= questions.length;

  // Показать/скрыть кнопку для отправки ответа, если последний вопрос
  document
    .getElementById("submit-answer-container")
    .classList.toggle("d-none", endIndex < questions.length);
}

// Функция для отправки ответа на сервер
function submitAnswer() {
  // Собираем выбранные ответы
  questions.forEach((question, index) => {
    const selectedAnswer = document.querySelector(
      `input[name="answer-${index}"]:checked`
    );
    if (selectedAnswer) {
      userAnswers.push({
        questionId: question.id,
        selectedAnswer: selectedAnswer.value,
      });
    }
    console.log(userAnswers)
  });

  // Отправляем ответы на сервер
  fetch(submitUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      testId: testId,
      answers: userAnswers,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to submit answers");
      }
      return response.json();
    })
    .then((data) => {
      alert("Answers submitted successfully!");
      console.log("Server response:", data);
    })
    .catch((error) => {
      console.error("Error submitting answers:", error);
    });
}

// Событие для нажатия кнопки "Start Test"
document.getElementById("start-test-btn").addEventListener("click", () => {
  document.getElementById("start-test-container").classList.add("d-none");
  fetchTest();
});

// События для переключения страниц вопросов
document.getElementById("prev-question-btn").addEventListener("click", () => {
  if (currentPage > 0) {
    currentPage--;
    displayQuestions();
  }
});

document.getElementById("next-question-btn").addEventListener("click", () => {
  if ((currentPage + 1) * questionsPerPage < questions.length) {
    currentPage++;
    displayQuestions();
  }
});

// Событие для отправки ответа
document
  .getElementById("submit-answer-btn")
  .addEventListener("click", submitAnswer);
