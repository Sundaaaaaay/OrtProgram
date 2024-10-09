// Функция для получения параметров из URL
function getQueryParams() {
  const params = new URLSearchParams(window.location.search);
  return {
    testId: params.get("testId"),
  };
}

// URL вашего API для получения теста по ID
const testId = getQueryParams().testId;
const apiUrl = `https://localhost:44340/ort/tests/gettest${testId}`;

// Функция для получения теста с вопросами с сервера
function fetchTest() {
  fetch(apiUrl)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Received data:", data); // Отладка: выводим полученные данные
      // Вызов функции для отображения вопросов
      if (data.questions && Array.isArray(data.questions)) {
        displayQuestions(data.questions);
      } else {
        console.error(
          "Expected an array of questions but got:",
          data.questions
        );
        document.getElementById("error-message").classList.remove("d-none");
      }
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
      // Показать сообщение об ошибке
      document.getElementById("error-message").classList.remove("d-none");
    })
    .finally(() => {
      // Скрыть индикатор загрузки
      document.getElementById("loader").style.display = "none";
    });
}

// Функция для отображения вопросов
function displayQuestions(questions) {
  const questionList = document.getElementById("question-list");

  // Очистка списка перед обновлением
  questionList.innerHTML = "";

  // Итерация по вопросам и создание элементов списка
  questions.forEach((question) => {
    const listItem = document.createElement("li");
    listItem.classList.add("list-group-item");

    // Добавляем вопрос и ответы в элемент списка
    listItem.innerHTML = `
                    <strong>Question:</strong> ${question.questionText} <br>
                    <strong>Answers:</strong>
                    <ul>
                        <li>${question.firstAnswer}</li>
                        <li>${question.secondAnswer}</li>
                        <li>${question.thirdAnswer}</li>
                    </ul>
                    <strong>Right Answer:</strong> ${question.rightAnswer}
                `;

    questionList.appendChild(listItem);
  });
}

// Вызов функции для получения и отображения вопросов при загрузке страницы
window.onload = fetchTest;
