"use client"
import Image from "next/image";
import styles from "./page.module.css";
import { useState } from "react";

export default function Home() {
  const [name, setName] = useState("");
  const [question, setQuestion] = useState({});
  const [questions, setQuestions] = useState([]);
  const start = async () => {
    const response = await fetch(`api/questions`);
    debugger
    const questionData = await response.json(); // Await the JSON parsing
    setQuestions(questionData.data); // Assuming the API response has a `data` property
    console.log(`Start Game for ${name} with question ${JSON.stringify(question)}`);
  };

  return (
    <div className={styles.page}>
      <div style={{ flexbox: 1, alignItems: "center"}}>
        <div>
          <input placeholder="Name" value={name} onChange={(e)=>setName(e.target.value)}></input>
        </div>
        <div>
          <button onClick={()=>start()}>Start</button>
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
