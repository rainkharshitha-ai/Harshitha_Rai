import { useEffect, useState } from "react";
import styles from "./search.module.css";

const URL = "https://api.spoonacular.com/recipes/complexSearch";
const API_KEY = "abb44c6a399b4880a4915e2a230bb2a5";

export default function Search({ foodData, setFoodData }) {
  const [query, setQuery] = useState("pizza");

  useEffect(() => {
    async function fetchFood() {
      const res = await fetch(`${URL}?query=${query}&apiKey=${API_KEY}`);
      const data = await res.json();
      console.log(data.results);
      setFoodData(data.results);
    }
    fetchFood();
  }, [query]);

  return (
    <div className={styles.searchContainer}>
      <select
        className={styles.input}
        value={query}
        onChange={(e) => setQuery(e.target.value)}>
       <option value="pizza">Pizza</option>
        <option value="burger">Burger</option>
        <option value="pasta">Pasta</option>
        <option value="salad">Salad</option>
        <option value="noodles">Noodles</option>
        <option value="rice">Rice</option>
        <option value="biryani">Biryani</option>
        <option value="sandwich">Sandwich</option>
        <option value="soup">Soup</option>
        <option value="ice cream">Ice Cream</option>
        <option value="cake">Cake</option>
        <option value="donut">Donut</option>
        <option value="fried chicken">Fried Chicken</option>
        <option value="tacos">Tacos</option>
        <option value="pancake">Pancake</option>
        <option value="waffles">Waffles</option>
        <option value="curry">Curry</option>
        <option value="fish">Fish</option>
        <option value="mutton">Mutton</option>
        <option value="beef">Beef</option>
        <option value="egg">Egg</option>
        <option value="bread">Bread</option>
        <option value="cheese">Cheese</option>
        <option value="sausage">Sausage</option>
        <option value="fries">Fries</option>
        <option value="chocolate">Chocolate</option>
        <option value="coffee">Coffee</option>
        <option value="tea">Tea</option>
        <option value="smoothie">Smoothie</option>
        <option value="juice">Juice</option>
        <option value="pudding">Pudding</option>
        <option value="cookies">Cookies</option>
        <option value="dumplings">Dumplings</option>
        <option value="spring rolls">Spring Rolls</option>
        <option value="sushi">Sushi</option>
        <option value="shawarma">Shawarma</option>
        <option value="kebab">Kebab</option>
        <option value="omelette">Omelette</option>
        <option value="lasagna">Lasagna</option>
        <option value="spaghetti">Spaghetti</option>
        <option value="idli">Idli</option>
        <option value="poha">Poha</option>
        <option value="upma">Upma</option>
        <option value="samosa">Samosa</option>
        <option value="vada">Vada</option>
      </select>
    </div>
  );
}
