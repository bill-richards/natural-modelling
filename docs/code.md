# Getting the source

Because some of the solutions in this repository have dependencies on shared libraries, those libraries (which can be) are included as submodules. 
When you get the source using

```cmd
git clone https://github.com/bill-richards/natural-modelling.git
```

be sure to also call

```cmd
git submodule update --init --recursive
```

## Updating the source

It is important to note that a _**submodule**_ is linked to a repository at a specific version

If the submodules do not refelect the current state of the super repositories -because commits have been made to those super repositories- then you will most likely want to update the submodules to incorporate those changes.

### Update all submodules

When you need to update all of the submodules (to be point to the current HEAD of their repositories), run

```cmd
git submodule update --remote --merge
```

### Update individual submodules

When you want to update an individual submodule (and not ALL submodules) run the following

```cmd
git submodule update <path-to-submodule>
```

so, for example, to update **only** the _gsdc-common_ submodule within the _evolution_ folder

```cmd
git submodule update evolution/libraries/gsdc-common
```

### After updating the submodule(s)

Don't forget that you will also have to **push** those updates back to the remote repository

```cmd
git push
```

# Making changes to a submodule

This could be made a whole lot easier, I'm sure, but we have what we have and so, you will need to follow the instructions carefully.

If we work on a feature, it is possible or even probable that we will need to also extend the functionality expressed within a submodule project. Do your work and check it in!

## No matter how many submodules contain change

You can add all of your changes to submodules either from within the submodule's root (i.e. it's local folder), which you would then need to do **_for each submodule with changes_**; alternatively, you can add the changes for all of those submodules **_from the root of the superproject_**, as follows.

```cmd
REM iterates over all contained submodules and adds changes
git submodule foreach git add . 
```

### For each submdule containing local changes

```cmd
REM move to the submodule's directory
cd <submodule-name>

REM commit all changes for this submodule
git commit -a -m "sub module change description"

REM return to the project root
cd ..

REM adding everything here includes adding the committed submodule
git add .

REM commit the superproject
git commit -a -m "Committing submodule changes from superproject"

REM push all changes to the remote repository
git push --recurse-submodules=on-demand
```

### Another way to get all up to date submodules

```cmd
git submodule foreach git pull origin development
```

#### **_To be verified_**

[] Using the Visual Studio Git extension, check that all changes are propagated back to their respective repositories.
