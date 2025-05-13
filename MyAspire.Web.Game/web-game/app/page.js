"use client"
import Image from "next/image";
import styles from "./page.module.css";
import { useState } from "react";

export default function Home() {
  const [name, setName] = useState("");
  const [question, setQuestion] = useState({});
  const [questions, setQuestions] = useState([]);
  const start = async () => {
    const response = await fetch(`api-service/questions`);
    debugger
    const questionData = await response.json(); // Await the JSON parsing
    setQuestions(questionData.data); // Assuming the API response has a `data` property
    console.log(`Start Game for ${name} with question ${JSON.stringify(question)}`);
  };

  const answer = async () => {
    const response = await fetch(`api-game/answer`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ 
        QuestionId: 1, 
        Time: 1.02,
        Value: 10        
       }),
    });
    console.log(`Ok? ${response.ok}`);
  }

  return (
    <div className={styles.page}>
      <div style={{ flexbox: 1, alignItems: "center"}}>
        <div>
          <input placeholder="Name" value={name} onChange={(e)=>setName(e.target.value)}></input>
        </div>
        <div>
          <button onClick={()=>start()}>Start</button>
          <button onClick={()=>answer()}>answer</button>
        </div>
      </div>
      <div>{JSON.stringify(questions)}</div>
      <div>
        {questions && questions.map((q, i) => (
          <div key={i} style={{ display: "flex", flexDirection: "column", alignItems: "center" }}>
            <div>{q.text}</div>
          </div>
        ))}
      </div>
    </div>
  );
}
