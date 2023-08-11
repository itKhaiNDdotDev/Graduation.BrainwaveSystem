from sklearn import svm
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.tree import DecisionTreeClassifier
import numpy as np
file1 = pd.read_csv('Awake.csv')
file2 = pd.read_csv('drowsiness1.csv')

X = pd.concat([file1, file2])
y = pd.concat([pd.Series([1]*len(file1)), pd.Series([0]*len(file2))]) # 1 la co tinh tao, 0 la buon ngu
print(X)
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
model = svm.SVC(kernel="linear")
model.fit(X_train, y_train)
train_score = model.score(X_train, y_train)
score = model.score(X_test, y_test)
print("Train score: ", train_score)
print("Test score: ",score)
# from sklearn.model_selection import KFold
# from sklearn.metrics import accuracy_score
#
# # Define number of folds
# k = 5
#
# # Initialize KFold object
# kf = KFold(n_splits=k)
#
# # Initialize list to store accuracy scores
# accuracy_list = []
#
# # Loop through each fold
# for train_index, test_index in kf.split(X):
#     # Split data into training and testing sets
#     X_train, X_test = X.iloc[train_index], X.iloc[test_index]
#     y_train, y_test = y.iloc[train_index], y.iloc[test_index]
#
#     # Initialize model
#
#     # Fit the model on the training set
#     model.fit(X_train, y_train)
#
#     # Make predictions on the testing set
#     y_pred = model.predict(X_test)
#
#     # Calculate accuracy score
#     accuracy = accuracy_score(y_test, y_pred)
#
#     # Append accuracy score to the list
#     accuracy_list.append(accuracy)
#
# # Calculate average accuracy score
# avg_accuracy = np.mean(accuracy_list)
#
#("Average Accuracy Score:", avg_accuracy)