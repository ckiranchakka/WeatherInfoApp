<template>
<div>
    <div class="page-header">
        <h1>Weather Forecast</h1>
    </div>
    <form @submit.prevent="getWeather(inputValue)" class="form">
        <div class="text-center">
            <div class="radioinline">
                <label for="City">City</label>
                <input type="radio" id="cityid" value="City" v-model="searchCategory" v-on:click="setPlaceholder('city')" />
                <span class="checkmark"></span>
            </div>
            <div class="radioinline">
                <label for="ZipCode">ZipCode</label>
                <input type="radio" id="zipcodeid" value="ZipCode" v-model="searchCategory" v-on:click="setPlaceholder('ZipCode')" />
                <span class="checkmark"></span>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-12 colxs-12">

            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <input type="text" id="txtSearch" class="autosearchfield" @keyup="filterAutoComplete" v-model="inputValue" placeholder=" Enter City " />
                <ul class="autoComplete" :class="isHistoryShow? 'showDiv autotext' : 'hideDiv'">
                    <li v-for="(inputItem, i) in filterData" :key="i">
                        <span @click="setItem(inputItem)">{{inputItem}}</span>
                    </li>
                </ul>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-12 colxs-12">
                <button type="submit" class="autoformbtn ML30">Search</button>
            </div>
        </div>

    </form>
    <div>
        <h3 v-if="showError" class="selectedlocation" centered>{{errorText}}</h3>
    </div>

    <div class="row">
        <div class="container">
            <h3  class="selectedlocation text-center" centered>{{city}} {{ country}}</h3>
            <br />
            <div class="row">
                <div  class="col-lg-3 col-md-3 col-sm-4 colxs-12">
                    <div v-if="weather_Day.temperature" class="card bg-light text-dark col-sm text-center">
                        <h4 class="card-text"> Today </h4>
                        <h5 class="card-text">{{weather_Day.day}}</h5>
                        <h5 class="card-text">{{weather_Day.date_time}}</h5>
                        <h5 class="card-text">{{weather_Day.description}}</h5>
                        <img :src="weather_Day.icon" v-bind:alt="weather_Day.icon" style="align-self:center" />
                        <h5 class="card-text">{{weather_Day.temperature.celsiusCurrent}} &#8451;</h5>
                        <h5 class="card-text">{{weather_Day.pressure}} hpa</h5>
                        <h5 class="card-text">{{weather_Day.humdity}}%</h5>
                        <h5 class="card-text">{{weather_Day.speed}} kn</h5>
                       </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-4 colxs-12">
                     <div v-if="weather_Night.temperature" class="card bg-light text-dark col-sm text-center">
                        <h4 class="card-text"> Tonight</h4>
                        <h5 class="card-text">{{weather_Night.day}}</h5>
                        <h5 class="card-text">{{weather_Night.date_time}}</h5>
                        <h5 class="card-text">{{weather_Night.description}}</h5>
                        <img :src="weather_Night.icon" v-bind:alt="weather_Night.icon" style="align-self:center" />
                        <h5 class="card-text">{{weather_Night.temperature.celsiusCurrent}} &#8451;</h5>
                        <h5 class="card-text">{{weather_Night.pressure}} hpa</h5>
                        <h5 class="card-text">{{weather_Night.humdity}}%</h5>
                        <h5 class="card-text">{{weather_Night.speed}} kn</h5>
                     </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-4 colxs-12">
                    <div id="Chartid">
                        <div class="container">
                            <div class="my-5">
                                <div>
                                    <canvas id="myChart"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="table-responsive text-center  P40">
            <table class="table table-bordered">
                <thead style="backgroundColor:#403b5f; color:#fff">
                    <tr>
                        <th>Day</th>
                        <th>Description</th>
                        <th>High/Low</th>
                        <th>Wind</th>
                        <th>Humidity</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in weatherForecasts" :key="item.date" style="background:#DCCFD5">
                        <td>
                            <a href="#" @click="setChartItem(item)">
                                <div class="d_info">
                                    <span>{{item.day}}</span>
                                    <br />
                                    <span>{{item.date}}</span>
                                </div>
                                <div class="w_icon">
                                    <span>
                                        <img :src="item.icon" />
                                    </span>
                                </div>
                            </a>
                        </td>
                        <td>
                            <div>
                                <span>{{item.description}}</span>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span>{{item.temperature.celsiusCurrent}}</span>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span>{{item.humdity}}%</span>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span>{{item.speed}} km/h</span>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

    </div>
</div>
</template>

<script>
import axios from "axios"
import Chart from "chart.js"
export default {
    name: "weather",
    data: function () {
        return {
            url: "",
            weatherData: {},
            weather_Day: {},
            weather_Night: {},
            weatherForecasts: [],
            dayParts: [],
            dates: [],
            temps: [],
            inputValue: "",
            placeholder: " Enter City ",
            errorText: "",
            showError: false,
            country: "",
            searchCategory: "City",
            selected: "",
            city: "",
            chart: null,
            searchHistoryList: [],
            inputString: "",
            isHistoryShow: false,
            searchOptions: [{
                    SearchName: "City",
                    SearchId: "1"
                },
                {
                    SearchName: "Zipcode ",
                    SearchId: "2"
                }
            ]
        }
    },
    computed: {
        filterData() {
            if (this.inputString !== "") {
                if (this.inputValue === "") {
                    this.isHistoryShow = false
                    return this.searchHistoryList
                } else {
                    this.isHistoryShow = true
                    this.showError = false
                    return this.searchHistoryList.filter(item => {
                        return item.indexOf(this.inputString) !== -1
                    })
                }
            } else {
                this.isHistoryShow = false
                return this.searchHistoryList
            }

        }

    },
    mounted() {
        if (localStorage.getItem('searchHistory')) {
            try {
                this.searchHistoryList = JSON.parse(localStorage.getItem('searchHistory'))
            } catch (e) {
                localStorage.removeItem('searchHistory')
            }
        }
        this.getWeather()
    },
   created(){
        document.onclick = this.documentClick
    },
    methods: {
        documentClick(e){
            this.isHistoryShow = false 
        },
        setItem(inputItem) {
            this.inputValue = inputItem
            this.isHistoryShow = false
            this.showError = false
        },
        setPlaceholder(val) {
            if (val === "city") {
                document.getElementById("txtSearch").placeholder = "Enter City"
            } else if (val === "ZipCode") {
                document.getElementById("txtSearch").placeholder = "Enter ZipCode and Country Code (eg: 04109,DE)"
            }
            this.showError = false
            this.inputString = ""
        },
        setChartItem(selectedCard) {
            this.dates = selectedCard.periods.map(list => {
                return list.date_time
            })
            this.temps = selectedCard.periods.map(list => {
                return list.temperature.celsiusCurrent
            })
            this.weather_Day = selectedCard.periods[0]
            var periodslenght = selectedCard.periods.length
            this.weather_Night = selectedCard.periods[periodslenght - 1]
            this.weatherData = selectedCard
            this.getchartdata()
        },
        checkForm: function (e) {
            if (this.inputValue === "") {
                return false
            }

            e.preventDefault()
            return true
        },
        getWeather: function (input) {
            if (input !== "") {
                this.inputValue = ""
            }
            if (input === "" || input === undefined) {
                input = 'Leipzig'
            }
            if (this.searchHistoryList.indexOf(input) === -1) {
                this.searchHistoryList.push(input)
            }
            localStorage.setItem("searchHistory", JSON.stringify(this.searchHistoryList))
            this.url = ""
            this.isHistoryShow = false
            if (this.searchCategory === "City") {
               this.url = "http://localhost:49178/api/weatherforecast/search?City=" + input + "&isZipCode=false"
            } else if (this.searchCategory === "ZipCode") {
                this.url =
                    "http://localhost:49178/api/weatherforecast/search?ZipCode=" +
                    input +
                    "&isZipCode=true"
            }
            axios
                .get(this.url)
                .then(response => {
                    this.responseCode = undefined
                    if (response.data.forecasts === null) {
                        this.errorText = "Not Found"
                    } else {
                        this.weatherData = response.data.forecasts[0]
                        this.weather_Day = response.data.forecasts[0].periods[0]
                        var periodslenght = response.data.forecasts[0].periods.length
                        this.weather_Night = response.data.forecasts[0].periods[periodslenght - 1]
                        this.weatherForecasts = response.data.forecasts
                        this.setChartItem(response.data.forecasts[0])
                        this.city = response.data.city
                        this.country = " - " + response.data.country

                        this.dates = response.data.forecasts[0].periods.map(list => {
                            return list.date_time
                        })
                        this.temps = response.data.forecasts[0].periods.map(list => {
                            return list.temperature.celsiusCurrent
                        })
                      
                        this.getchartdata()
                        
                    }
                })
                .catch(error => {
                    if (error) {
                        this.errorText = "Error in finding this City or ZipCode !"
                        this.showError = true
                    }
                })
            
        },
        filterAutoComplete($event) {
            this.inputString = $event.srcElement.value
        },
        getchartdata () {
            this.showError = false
            var ctx = document.getElementById("myChart")
            this.chart = new Chart(ctx, {
                type: "line",
                data: {
                    labels: this.dates,
                    datasets: [{
                        label: "Avg. Temp",
                        backgroundColor: "rgba(54, 162, 235, 0.5)",
                        borderColor: "rgb(54, 162, 235)",
                        fill: false,
                        data: this.temps
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: "Weather Graph"
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                var label = data.datasets[tooltipItem.datasetIndex].label || ""

                                if (label) {
                                    label += ": "
                                }

                                label += Math.floor(tooltipItem.yLabel)
                                return label + "°F"
                            }
                        }
                    },
                    scales: {
                        xAxes: [{
                            type: "time",
                            time: {
                                unit: "hour",
                                displayFormats: {
                                    hour: "M/DD @ hA"
                                },
                                tooltipFormat: "MMM. DD @ hA"
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Date/Time"
                            }
                        }],
                        yAxes: [{
                            scaleLabel: {
                                display: true,
                                labelString: "Temperature (°F)"
                            },
                            ticks: {
                                callback: function (value, index, values) {
                                    return value + "°F"
                                }
                            }
                        }]
                    }
                }
            })
        }
    }
}
</script>

<style scoped>
.searchbar {
    width: 100%;
}

@media (min-width: 1200px) {
    .container {
        max-width: 1150px;
    }
}

@media (min-width: 990px) {
    .ML30 {
        margin-left: -30px !important;
    }
}

form.form {
    position: relative;
    background-color: #e2e2e2;
    padding: 30px 10px;
}

.autosearchfield {
    width: 100%;
    padding: 10px;
    border: 1px solid #b5b5b5;
}

.autoformbtn {
    background-color: #ff4700;
    color: #fff;
    padding: 10px;
    border: 0;
    text-transform: uppercase;
    box-shadow: none;
}

.autoComplete {
    list-style-type: none;
    padding-left: 5px;
    margin: 0;
    position: absolute;
    z-index: 9;
    background-color: #fff;
    border: 1px solid #ccc;
    width: 100%;
    top: 120px;
    left: 0;
    right: 0;
    margin: 0 auto;
    display: none;
}

.autoComplete li {
    text-align: left;
}

.autoComplete li span {
    padding: 10px;
    display: block;
    border-bottom: 1px solid #ccc;
}

.autoComplete li span:hover {
    background-color: #f4f4f4;
}

.showDiv {
    display: block !important;
}

.hideDiv {
    display: none !important;
}

#grid {
    width: 900px;
    position: absolute;
    margin: 0 auto;
    left: 0;
    right: 0;
}

.bg-light {
    background-color: #c0cdec !important;
}

.card {
    border: 1px solid #8ea7c1;
    border-radius: 0px;
    /* border-left: 0px; */
    padding: 10px;
}

h5 {
    font-size: 15px;
}

.selectedlocation {
    display: inline-block;
    background: #585858;
    color: #fff;
    padding: 5px 15px;
    font-size: 20px;
    margin-bottom: 20px;
}

.radioinline {
    display: inline-block;
    position: relative;
    width: 96px;
    text-align: left;
    margin-bottom: 20px;
    padding-left: 35px;
    cursor: pointer;
}

.radioinline label {
    /* margin-left: 32px; */
    font-size: 18px;
    margin-bottom: 0;
}

/* Hide the browser's default radio button */
.radioinline input {
    position: absolute;
    opacity: 0;
    cursor: pointer;
    left: 0;
    width: 100%;
    height: 25px;
    top: 0;
    z-index: 1;
}

/* Create a custom radio button */
.checkmark {
    position: absolute;
    top: 0;
    left: 0;
    height: 25px;
    width: 25px;
    background-color: #adadad;
    border-radius: 50%;
}

/* On mouse-over, add a grey background color */
.radioinline:hover input~.checkmark {
    background-color: #ccc;
}

/* When the radio button is checked, add a blue background */
.radioinline input:checked~.checkmark {
    background-color: #2196f3;
}

/* Create the indicator (the dot/circle - hidden when not checked) */
.checkmark:after {
    content: "";
    position: absolute;
    display: none;
}

/* Show the indicator (dot/circle) when checked */
.radioinline input:checked~.checkmark:after {
    display: block;
}

/* Style the indicator (dot/circle) */
.radioinline .checkmark:after {
    top: 9px;
    left: 9px;
    width: 8px;
    height: 8px;
    border-radius: 50%;
    background: white;
}

.table-bordered th,
.table-bordered td {
    vertical-align: middle;
}

.d_info,
.w_icon {
    display: inline-block;
    vertical-align: middle;
}

.P40 {
    padding: 40px;
}

.P15 {
    padding: 15px;
}

.autotext {
    margin-top: -74px;
    margin-left: 15px;
    width: 97%;
}
</style>
