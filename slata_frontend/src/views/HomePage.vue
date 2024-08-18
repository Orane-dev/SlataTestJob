<template>
    <div class="home-container">
        <div class="top-container">
            <h2>Slata Candidates</h2>
            <button class="danger-btn" @click="logout">Logout</button>
        </div>
        
        <div style="margin-top: 10px;">
            <button v-if="isHR" @click="showForm = true">Create</button>
            <div v-if="showForm">
                <div class="task-card">
                    <h2>Create Candidate</h2>
                    <form @submit.prevent="submitForm">
                        <p>
                            <label>
                                Full Name:
                                <input style="min-width: 300px;" v-model="newCandidate.fullName" type="text" required />
                            </label>
                        </p>
                        <p>
                            <label>
                                Phone Number:
                                <input v-model="newCandidate.phoneNumber" type="text" required />
                            </label>
                            <label>
                                Job Position:
                                <input v-model="newCandidate.jobPosition" type="text" required />
                            </label>
                            <label>
                                Department:
                                <select v-model="newCandidate.department" required>
                                    <option>HR</option>
                                    <option>IT</option>
                                    <option>Marketing</option>
                                    <option>Assortment</option>
                                </select>
                            </label>
                            <label>
                                Interview Date:
                                <input v-model="newCandidate.iterviewDate" type="date" required />
                            </label>
                        </p>
                        <p>
                            <label>
                                Interview Manager:
                                <input v-model="newCandidate.interviewManager" @input="onInterviewerInput" type="text" placeholder="type 'Director'"
                                    required />
                                <ul v-if="searchResult.length" class="search-results">
                                    <li v-for="result in searchResult" @click="selectInterviewer(result)">
                                        {{ result.fullname }}
                                    </li>
                                </ul>
                                
                            </label>
                        </p>
                        <p>
                            <button type="submit">Submit</button>
                            <button class="cancel-btn" @click="showForm = false">Cancel</button>
                        </p>
                    </form>
                </div>
            </div>
        </div>
        <div class="table-container ">
            <table>
                <thead>
                    <tr>
                        <th>Full name</th>
                        <th>Phone number</th>
                        <th>Department</th>
                        <th>Position</th>
                        <th>Manager</th>
                        <th>Interview date</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="candidate in candidates">
                        <td>{{ candidate.fullName }}</td>
                        <td>{{ candidate.phoneNumber }}</td>
                        <td>{{ candidate.department }}</td>
                        <td>{{ candidate.jobPosition }}</td>
                        <td>{{ candidate.interviewManager }}</td>
                        <td>{{ candidate.iterviewDate }}</td>
                        <td>
                            <button @click="toggleDetails(candidate.id, candidate.fullName)">Test Job</button>
                            <button v-if="isHR" class="danger-btn" @click="deleteCandidate(candidate.id)">Delete</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-if="selectedCandidate !== null" class="task-card">
            <div v-if="taskDetails !== null">
                <h3>Test Job Details for: {{ selectedCandidate.name }}</h3>
                <form @submit.prevent="saveTestJob">
                    <p>
                        <label>
                            <strong>
                                Start Date:
                            </strong>
                            <input v-model="taskDetails.startDate" type="date" required>
                        </label>
                    </p>
                    <p>
                        <label>
                            <strong>
                                End Date:
                            </strong>
                            <input v-model="taskDetails.endDate" type="date" required>
                        </label>
                    </p>
                    <p>
                        <label><strong>Status: </strong>
                            <select v-model="taskDetails.status">
                                <option v-for="(label, value) in jobStatus" :key="value" :value="label">
                                    {{ label }}
                                </option>
                            </select>
                        </label>
                    </p>
                    <p>
                        <label>
                            <strong>
                                ReceiveDate:
                            </strong>
                            <input v-model="taskDetails.reciveDate" type="date">
                        </label>
                    </p>
                    <p>
                        <label>
                            <strong>
                                Description:
                            </strong>
                            <input style="min-width: 450px;" v-model="taskDetails.description" type="text">
                        </label>
                    </p>
                    <button type="submit">Save</button>
                    <button class="cancel-btn" type="button" @click="closeDetails">Cancel</button>
                    <button v-if="isHR" class="danger-btn" type="button" @click="deleteTestJob">Delete</button>
                </form>

            </div>
            <div v-else>
                <h3>Create Test Job for: {{ selectedCandidate.name }}</h3>
                <form @submit.prevent="createTestJob">
                    <p>
                        <label>
                            <strong>
                                StartDate:
                            </strong>
                            <input v-model="newTestJob.startDate" type="date" required>
                        </label>
                    </p>
                    <p>
                        <label>
                            <strong>End Date:
                            </strong>
                            <input v-model="newTestJob.endDate" type="date" required>
                        </label>
                    </p>
                    <p>
                        <label>
                            <strong>Status: </strong>
                            <select v-model="newTestJob.status" required>
                                <option v-for="(label, value) in jobStatus" :key="value" :value="label">
                                    {{ label }}
                                </option>
                            </select>
                        </label>
                    </p>
                    <p>
                        <label>
                            <strong>
                                Description:
                            </strong>
                            <input v-model="newTestJob.description" type="text">
                        </label>
                    </p>
                    <button type="submit">Submit</button>
                    <button class="cancel-btn" type="button" @click="closeDetails">Cancel</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            candidates: [],
            selectedCandidate: null,
            taskDetails: null,
            showForm: false,
            newCandidate: {
                fullName: '',
                phoneNumber: '',
                jobPosition: '',
                department: '',
                interviewDate: '',
                interviewManager: ''
            },
            jobStatus: {
                0: 'None',
                1: "In Progress",
                2: "Postponed",
                3: "In Progress, Postponed",
                4: "Done"
            },
            newTestJob: {
                startDate: "",
                endDate: "",
                reciveDate: null,
                status: 0,
                description: null,
            },
            searchResult: [],
            isHR: localStorage.getItem('department') === 'HR',
	    apiUrl: 'https://localhost:7098',
        };
    },

    methods: {
        async toggleDetails(candidateId, candidateFullName) {
            if (this.selectedCandidate && this.selectedCandidate.id === candidateId) {
                this.closeDetails();
                return;
            }

            this.selectedCandidate = {
                id: candidateId,
                name: candidateFullName
            };

            await this.loadTestJob(candidateId);
        },

        async deleteCandidate(candidateId) {
            try {
                const response = await axios.delete(`${this.apiUrl}/Candidate?id=${candidateId}`)
                const index = this.candidates.findIndex(x => x.id === candidateId);
                if (index !== -1) {
                    this.candidates.splice(index, 1);
                }

            } catch (error) {
                console.error(error);
            }
        },

        async deleteTestJob(){
            try{
                const response = await axios.delete(`${this.apiUrl}/TestJob?id=${this.taskDetails.id}`)
                this.closeDetails();
                alert("Success");         
            } catch (error) {
                console.error(error);
            }             
        },

        async loadTestJob(candidateId) {
            try {
                const response = await axios.get(`${this.apiUrl}/TestJob?candidateId=${candidateId}`);
                const status = response.data.status;
                const statusLabel = this.jobStatus[status];

                this.taskDetails = {
                    ...response.data,
                    status: statusLabel
                };
            } catch (error) {
                console.log(error);
                this.taskDetails = null;
            }
        },

        async onInterviewerInput() {
            try {
                if (this.newCandidate.interviewManager.length > 2) {
                    const response = await axios.get(`${this.apiUrl}/SearchEmployeesByName?fullName=${this.newCandidate.interviewManager}`)
                    this.searchResult = response.data
                }
            }
            catch (error) {
                console.error(error);
            }
        },

        selectInterviewer(interviewer) {
            this.newCandidate.interviewManager = interviewer.fullname;
            this.searchResult = [];
        },

        async saveTestJob() {
            try {
                const status = Object.keys(this.jobStatus).find(key => this.jobStatus[key] === this.taskDetails.status);
                const testJobModel = {
                    ...this.taskDetails,
                    status: parseInt(status, 10)
                };

                console.log(testJobModel);
                await axios.put(`${this.apiUrl}/TestJob`, testJobModel);
                alert("Success");
            } catch (error) {
                console.log('Error', error);
            }
        },

        async createTestJob() {
            try {
                const status = Object.keys(this.jobStatus).find(key => this.jobStatus[key] === this.newTestJob.status);
                const testJobModel = {
                    id: 0,
                    candidateId: this.selectedCandidate.id,
                    startDate: this.newTestJob.startDate,
                    endDate: this.newTestJob.endDate,
                    reciveDate: this.newTestJob.reciveDate,
                    status: parseInt(status, 10),
                    description: this.newTestJob.description
                };

                await axios.post(`${this.apiUrl}/TestJob`, testJobModel);

                this.newTestJob = {
                    startDate: "",
                    endDate: "",
                    reciveDate: null,
                    status: 0,
                    description: null,
                };

                await this.loadTestJob(testJobModel.candidateId);
            } catch (error) {
                console.error("Error while create test job", error);
            }
        },

        closeDetails() {
            this.selectedCandidate = null;
            this.taskDetails = {};
        },

        logout(){
            localStorage.removeItem('authToken');
            localStorage.removeItem('department');
            location.reload();
        },

        async submitForm() {
            try {
                const responseCandidate = await axios.post(`${this.apiUrl}/Candidate`, this.newCandidate);
                this.candidates.push(responseCandidate.data);

                this.showForm = false;
                this.newCandidate = {
                    fullName: '',
                    phoneNumber: '',
                    jobPosition: '',
                    department: '',
                    interviewDate: '',
                    interviewManager: ''
                };
            } catch (error) {
                alert(error.response.data);
                console.error('Error creating candidate:', error);
            }
        },
    },

    mounted() {
        
        const userDepartment = localStorage.getItem('department');
        const url = userDepartment === 'HR'
            ? `${this.apiUrl}/Candidate`
            : `${this.apiUrl}/GetCandidatesByDepartment?department=${userDepartment}`;

        axios.get(url)
            .then(response => {
                this.candidates = response.data;
            })
            .catch(error => {
                console.error("Error while fetching candidates", error);
            });
    }
};
</script>

<style scoped>
table {
    width: 100%;
    text-align: center;
    border-collapse: collapse;
}

.table-container {
    margin-top: 10px;
    height: 450px;
    overflow-y: auto;
    border: 1px solid #ddd;

}

button {
    padding: 10px 20px;
    background: none;
    color: teal;
    border: 1px solid teal;
}

button:hover {
    background-color: teal;
    color: white;
}

p {
    margin-top: 15px;
}

td {
    height: 50px;

}

input, select {
    border: 1px solid teal;
    padding: 5px;
}

thead {
    height: 30px;
}

tbody {
    box-shadow: 1px 1px 1px 1px #999;
}

.task-card {
    border: 1px solid #ddd;
    border-radius: 5px;
    padding: 10px;
    margin: 10px 0;
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
}

.task-card h3 {
    margin: 0;
    padding-bottom: 10px;
    border-bottom: 1px solid #ddd;
}

.task-card button {
    margin-top: 10px;
}

.home-container {
    padding: 10px;
}

.cancel-btn {
    margin-left: 5px;
    border: 1px solid orange;
    color: orange;
}
    .cancel-btn:hover {
        color: white;
        background: orange;
    }

.danger-btn {
    margin-left: 5px;
    border: 1px solid red;
    color: red;
}
    .danger-btn:hover{
        color: white;
        background: red;
    }

.search-results {
    position: relative;
    background-color: white;
    border: 1px solid #ccc;
    max-height: 150px;
    overflow-y: auto;
}

.search-results li {
    padding: 8px;
}

.search-results li:hover {
    background-color: #f0f0f0;
}
.top-container {
    display: flex;
    justify-content: space-between; 
}
</style>
